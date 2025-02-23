using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.Model
{
    public record EventTicket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; }  // e.g., "General Admission", "VIP"
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public int AvailableCapacity { get; set; }
        public Event Event { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
