using System.ComponentModel.DataAnnotations.Schema;

namespace GetInItBackEnd.Entities;

public class Payment
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime? PaymentDate { get; set; }

   
    public string? Amount { get; set; }
    public string? StripePaymentId { get; set; }

    public string? PaymentStatus { get; set; }
}