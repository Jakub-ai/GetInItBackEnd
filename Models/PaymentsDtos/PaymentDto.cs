namespace GetInItBackEnd.Models.PaymentsDtos;

public class PaymentDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? Amount { get; set; }
    public string? StripePaymentId { get; set; }
    public string? PaymentStatus { get; set; }
}