using GetInItBackEnd.Entities;
using GetInItBackEnd.Models.Company;

namespace GetInItBackEnd.Models.Account;

public class AccountDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public string? CompanyName { get; set; }
    public string? Url { get; set; }
    
    

 
    
}