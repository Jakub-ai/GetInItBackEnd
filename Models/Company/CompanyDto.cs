using GetInItBackEnd.Entities;
using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Models.Address;

namespace GetInItBackEnd.Models.Company;

public class CompanyDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Url { get; set; }
    public string? Industry { get; set; }
    public string? Description { get; set; }
    public long Nip { get; set; }
    public int Regon { get; set; }
    public virtual List<AccountCompanyEmployeeDto>? Accounts { get; set; }
    public virtual AddressDto? Address { get; set; }
  
    

    /*public static CompanyDto FromCompanyDto(Company company)
    {
                  return new CompanyDto
                  {
                      Name = company.Name,
                      Url = company.Url
                  };
    }*/
}