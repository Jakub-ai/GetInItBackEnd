using GetInItBackEnd.Entities;
using GetInItBackEnd.Models.Company;

namespace GetInItBackEnd.Models.Account;

public class AccountDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }

    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string Url { get; set; }
    //public CompanyDto Company { get; set; }

    /*public static AccountDto FromAccount(Account account)
    {
        return new AccountDto
        {
            Id = account.Id,
            Name = account.Name,
            LastName = account.LastName,
            Email = account.Email,
            Role = account.Role,
            Company = new CompanyDto()
            {
                Name = account.Company.Name,
                Url = account.Company.Url
            }

        };
    }*/
    
}