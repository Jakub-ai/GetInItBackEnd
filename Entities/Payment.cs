using System.ComponentModel.DataAnnotations.Schema;

namespace GetInItBackEnd.Entities;

public class Payment
{
    public int Id { get; set; }
    public string PaymentDate { get; set; }
    public string Invoice { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal Amount { get; set; }
    public int CompanyId { get; set; }

    public virtual Company? Company { get; set; }
}