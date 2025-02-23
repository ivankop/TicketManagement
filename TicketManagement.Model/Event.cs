using System.Net.Sockets;

namespace TicketManagement.Model
{
    public record Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Venue { get; set; }
        public string Description { get; set; }       
        public List<EventTicket> EventTickets { get; set; }
    }
}
