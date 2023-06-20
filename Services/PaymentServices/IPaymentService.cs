using GetInItBackEnd.Models.PaymentsDtos;
using Stripe.Checkout;

namespace GetInItBackEnd.Services.PaymentServices;

public interface IPaymentService
{
     Task<Session> MakePayment();
   // public Task<int> PaymentToDatabase(HttpRequest request);

    public Task<int> CreatePayment(OfflinePaymentDto dto);
    public Task<IEnumerable<PaymentDto>> GetEveryPayment();
    public Task<PaymentDto> GetByIdPayment(int id);
    public Task<int> SavePayment();
}