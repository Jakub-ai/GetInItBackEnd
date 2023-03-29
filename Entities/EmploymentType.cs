namespace GetInItBackEnd.Entities;

public class EmploymentType
{
    public int Id { get; set; }
    public string Description { get; set; }

    public virtual List<Offer> Offers { get; set; }
}