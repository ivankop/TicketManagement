using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.Data;
using TicketManagement.Model;
using TicketManagement.Model.Contracts;
using TicketManagement.Model.Exceptions;

namespace TicketManagement.Biz
{
    public class TicketService : ITicketService
    {
        private readonly TicketDbContext _context;
        private readonly IPaymentService _paymentService;
        private readonly TimeSpan _reservationWindow;

        public TicketService(TicketDbContext context, IPaymentService paymentService, IOptions<TicketSettings> ticketSettings)
        {
            _context = context;
            _paymentService = paymentService;
            _reservationWindow = TimeSpan.FromMinutes(
                ticketSettings.Value.ReservationWindowMinutes);
        }
        public async Task<bool> CancelReservationAsync(int reservationId)
        {
            // cannot cancel if purchased, we need to implement the cancel and refund flow later
            var reservation = await _context.Reservations
                .Include(r => r.EventTicket)
                .FirstOrDefaultAsync(r => r.Id == reservationId && !r.IsPurchased);

            if (reservation == null) 
                throw new TicketServiceException("Reservation not found");

            reservation.EventTicket.AvailableCapacity += reservation.Quantity;
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetAvailableTicketsAsync(int eventTicketId)
        {
            var eventEntity = await _context.EventTickets
                .FirstOrDefaultAsync(e => e.Id == eventTicketId);
            return eventEntity?.AvailableCapacity ?? 0;
        }

        public async Task<List<Reservation>> GetReservationsByUserId(int userId)
        {
            var reservations = await _context.Reservations
                .Include(r => r.EventTicket)                
                .Where(r => r.UserId == userId).ToListAsync();

            if (reservations == null || !reservations.Any())
            {
                return new List<Reservation>(); // Return empty list instead of null
            }

            return reservations;
        }

        public async Task<bool> PurchaseTicketsAsync(int reservationId)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == reservationId && !r.IsPurchased);

            if (reservation == null)
                throw new TicketServiceException("Reservation not found");

            if (reservation.ExpiryTime < DateTime.UtcNow)
                throw new TicketServiceException("Reservation is expired");

            // Assume PaymentService exists
            bool paymentSuccess = await _paymentService.Process(reservation);

            if (paymentSuccess)
            {
                reservation.IsPurchased = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Reservation> ReserveTicketsAsync(int eventTicketId, int quantity, int userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var eventTicketEntity = await _context.EventTickets
                    .FirstOrDefaultAsync(e => e.Id == eventTicketId);

                if (eventTicketEntity == null || eventTicketEntity.AvailableCapacity < quantity)
                    throw new TicketServiceException("Not enough tickets available");

                var reservation = new Reservation
                {
                    EventTicketId = eventTicketId,
                    Quantity = quantity,
                    UserId = userId,
                    ReservationTime = DateTime.UtcNow,
                    ExpiryTime = DateTime.UtcNow.Add(_reservationWindow),
                    IsPurchased = false
                };

                eventTicketEntity.AvailableCapacity -= quantity;
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return reservation;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
