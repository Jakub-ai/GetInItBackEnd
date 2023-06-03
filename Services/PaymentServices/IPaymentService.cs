using Stripe.Checkout;

namespace GetInItBackEnd.Services.PaymentServices;

public interface IPaymentService
{
    Task<Session> MakePayment();
}