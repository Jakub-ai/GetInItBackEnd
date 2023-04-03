namespace GetInItBackEnd.Entities;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Industry { get; set; }
    public long Nip { get; set; }
    public int Regon { get; set; }
    public string Description { get; set; }
    public int AddressId { get; set; }
    public int AccountId { get; set; }

    public virtual Account Account { get; set; }
    public virtual Address Address { get; set; }
    public virtual List<Offer>? Offers { get; set; }
}