namespace GetInItBackEnd.Models.PaymentsDtos;

public class CreatePaymentDto
{
    public string PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string StripePaymentId { get; set; }
    public string PaymentStatus { get; set; }
}