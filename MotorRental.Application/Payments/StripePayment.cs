using Stripe.Checkout;


namespace MotorRental.UseCase.Payments
{
    public class StripePayment : IPayment
    {
        public bool CheckIfPay(Guid appointmentId)
        {
            // will code here

            return true;
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
                SuccessUrl = $"https://localhost:7225/api/Appointment/FinishPayment?id={appointmentId}&typePayment=Stripe",
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
