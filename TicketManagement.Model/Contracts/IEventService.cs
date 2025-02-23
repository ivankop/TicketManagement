using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.Model.Contracts
{
    public interface IEventService
    {
        Task<Event> CreateEventAsync(Event eventDto);
        Task<Event> UpdateEventAsync(Event eventDto);
        Task<EventTicket> UpdateEventTicketAsync(EventTicket eventTicketDto);
        Task<Event> CancelEventAsync(Event eventDto);
        Task<Event> GetEventAsync(int eventId);

    }
}
