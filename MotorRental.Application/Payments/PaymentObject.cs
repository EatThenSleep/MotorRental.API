using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Payments
{
    public class PaymentObject
    {
        public string redirectUrl { get; set; } = string.Empty;
        public string sessionId { get; set; } = string.Empty;
    }
}
