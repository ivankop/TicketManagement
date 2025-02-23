using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.Model
{
    public record Reservation
    {
        public int Id { get; set; }
        public int EventTicketId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public bool IsPurchased { get; set; }
        public DateTime ReservationTime { get; set; }
        public DateTime ExpiryTime { get; set; }       
        public User User { get; set; }
        public EventTicket EventTicket { get; set; }
    }
}
