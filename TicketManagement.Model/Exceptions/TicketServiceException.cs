using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.Model.Exceptions
{
    public class TicketServiceException : Exception
    {
        public TicketServiceException(string message)
        : base(message)
        {
        }

        public TicketServiceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
