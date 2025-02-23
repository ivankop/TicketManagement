using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.Model.Contracts
{
    public interface IUserService
    {
        string Verify(string username, string password);
        void Register(string username, string password, string role);
    }
}
