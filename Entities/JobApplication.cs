namespace GetInItBackEnd.Entities;

public class JobApplication
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string? Cv { get; set; }
    public string? Message { get; set; }
    public bool Rodo { get; set; }
    public int OfferId { get; set; }

    public virtual Offer Offer { get; set; }
}