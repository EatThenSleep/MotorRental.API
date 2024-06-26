using MotorRental.Entities;
using Stripe.Checkout;


namespace MotorRental.UseCase.Payments
{
    public class StripePayment : IPayment
    {
        public bool CheckIfPay(Appointment appointment)
        {
            // will code here
            var service = new SessionService();

            if (string.IsNullOrEmpty(appointment.sessionId))
            {
                return false;
            }

            Session session = service.Get(appointment.sessionId);

            if (session.PaymentStatus.ToLower() == "paid")
            {
                return true;
            }

            return false;
        }


        public PaymentObject ExecuteTransaction(Guid appointmentId, string item, int total)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"http://localhost:4200/confirm-payment/{appointmentId}",
                CancelUrl = "https://example.com/cancel",
            };

            var sessionLineItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = total*100,//20.00 -> 2000
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item
                    },

                },
                Quantity = 1,
            };
            options.LineItems.Add(sessionLineItem);

            var service = new SessionService();
            Session session = service.Create(options);

            return new PaymentObject { redirectUrl = session.Url, sessionId = session.Id};
        }
    }
}
