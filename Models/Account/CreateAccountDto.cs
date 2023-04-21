using System.ComponentModel.DataAnnotations;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Models.Company;

namespace GetInItBackEnd.Models.Account;

public class CreateAccountDto
{

    public string Name { get; set; }

    public string LastName { get; set; }
  
    public string Email { get; set; }
 
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public Role Role { get; set; }
    

    public CreateCompanyDto? CreateCompanyDto { get; set; }
  //  public Role Role { get; set; } = Role.EmployeeAccount;
   
    


    
}