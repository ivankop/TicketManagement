using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.Model
{
    public record User
    {
        public int Id { get; set; }        
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
