using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.Model.Exceptions
{
    public class UserServiceException : Exception
    {
        public UserServiceException(string message)
        : base(message)
        {
        }

        public UserServiceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
