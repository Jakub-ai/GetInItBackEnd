using Stripe.Checkout;

namespace GetInItBackEnd.Services.PaymentServices;

public interface IPaymentService
{
    Task<Session> MakePayment();
    public Task<int> PaymentToDatabase(HttpRequest request);
}