using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.Model.Exceptions
{
    public class EventServiceException: Exception
    {
        public EventServiceException(string message)
        : base(message)
        {
        }

        public EventServiceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
