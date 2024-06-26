using MotorRental.Entities;
using MotorRental.UseCase.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase
{
    public interface IPayment
    {
        bool CheckIfPay(Appointment appointment);
        PaymentObject ExecuteTransaction(Guid appointmentId, string item, int total);
    }
}
