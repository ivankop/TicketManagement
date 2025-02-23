using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.Model.Contracts
{
    public interface ITicketService
    {
        Task<Reservation> ReserveTicketsAsync(int eventTicketId, int quantity, int userId);
        Task<bool> PurchaseTicketsAsync(int reservationId);
        Task<bool> CancelReservationAsync(int reservationId);
        Task<int> GetAvailableTicketsAsync(int eventTicketId);
        Task<List<Reservation>> GetReservationsByUserId(int userId);
    }
}
