using GetInItBackEnd.Models.Address;

namespace GetInItBackEnd.Models.Company;

public class CreateCompanyDto
{
    public string Name { get; set; }
    public string Url { get; set; }
    public long Nip { get; set; }
    public int Regon { get; set; }
    
    public CreateAddressDto AddressDto { get; set; }
}