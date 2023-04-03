namespace GetInItBackEnd.Entities;

public class Technology
{
    public int Id { get; set; }
    public string Skill { get; set; }
    
    public virtual List<Offer>? Offers { get; set; }
    
}