using MotorRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Payments
{
    public class PayInCash : IPayment
    {
        public bool CheckIfPay(Appointment appointment)
        {
            return true;
        }

        public PaymentObject ExecuteTransaction(Guid appointmentId, string item, int total)
        {
            return null;
        }
    }
}
