using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.Model;
using TicketManagement.Model.Contracts;

namespace TicketManagement.Biz
{
    public class PaymentService : IPaymentService
    {
        public async Task<bool> Process(Reservation reservation)
        {
            return await Task.FromResult(true);
        }
    }
}
