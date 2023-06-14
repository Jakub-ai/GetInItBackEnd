using System.ComponentModel.DataAnnotations.Schema;

namespace GetInItBackEnd.Entities;

public class Payment
{
    public int Id { get; set; }
    public string PaymentDate { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal Amount { get; set; }
    public string StripePaymentId { get; set; }

    public string PaymentStatus { get; set; }
}