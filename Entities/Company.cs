namespace GetInItBackEnd.Entities;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Url { get; set; }
    public string? Industry { get; set; }
    public long Nip { get; set; }
    public int Regon { get; set; }
    public string? SubEndsAt { get; set; }
    public string? Description { get; set; }
    public int? AddressId { get; set; }
    
    
    public  Address? Address { get; set; }
    public  List<Offer>? Offers { get; set; }

    public  List<Account>? Accounts { get; set; }
}