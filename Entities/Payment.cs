namespace GetInItBackEnd.Entities;

public class Payment
{
    public int Id { get; set; }
    public string PaymentDate { get; set; }
    public string Invoice { get; set; }
    public decimal Amount { get; set; }
    public int AccountId { get; set; }

    public virtual Account? Account { get; set; }
}