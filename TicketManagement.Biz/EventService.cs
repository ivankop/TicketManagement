using Microsoft.EntityFrameworkCore;
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
    public class EventService : IEventService
    {
        private readonly TicketDbContext _context;
        
        public EventService(TicketDbContext context)
        {
            _context = context;
        }

        public async Task<Event> CancelEventAsync(Event eventEntity)
        {
            // need to handle cascade delete, refund if ticket is purchased
            throw new NotImplementedException();
        }

        public async Task<Event> CreateEventAsync(Event eventEntity)
        {
            var newEvent = _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();
            return newEvent.Entity;
        }

        public async Task<Event> GetEventAsync(int eventId)
        {
            var eventEntity = await _context.Events
                .Include(e => e.EventTickets)
                .FirstOrDefaultAsync(e => e.Id == eventId);
            if (eventEntity == null)
            {
                throw new EventServiceException("Event not found");
            }
            return eventEntity;
        }

        public async Task<Event> UpdateEventAsync(Event eventEntity)
        {
            var existingEvent = await _context.Events
                .Include(e => e.EventTickets)
                .FirstOrDefaultAsync(e => e.Id == eventEntity.Id);

            if (existingEvent == null) throw new EventServiceException("Event not found");

            _context.Entry(existingEvent).CurrentValues.SetValues(eventEntity);
            await _context.SaveChangesAsync();
            return existingEvent;
        }

        public async Task<EventTicket> UpdateEventTicketAsync(EventTicket eventTicketDto)
        {
            var existingEventTicket = await _context.EventTickets
                .FirstOrDefaultAsync(e => e.Id == eventTicketDto.Id);

            if (existingEventTicket == null) throw new EventServiceException("Event Ticket not found");
            existingEventTicket.Name = eventTicketDto.Name;
            existingEventTicket.Price = eventTicketDto.Price;
            existingEventTicket.Capacity = eventTicketDto.Capacity;
            existingEventTicket.AvailableCapacity = eventTicketDto.AvailableCapacity;

            // _context.Entry(existingEventTicket).CurrentValues.SetValues(existingEventTicket);
            await _context.SaveChangesAsync();
            return existingEventTicket;
        }
    }
}
