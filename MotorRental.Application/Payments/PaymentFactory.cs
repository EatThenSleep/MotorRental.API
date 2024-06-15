using MotorRental.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Payments
{
    public class PaymentFactory
    {
        public static IPayment CreateInstance(string role, string type)
        {
            if (role == SD.OWNER)
            {
                return new PayInCash();
            }
            else
            {
                if (type == SD.Payment_Stripe)
                {
                    return new StripePayment();
                }

                // will have many party payment
                return new StripePayment();
            }
        }
    }
}
