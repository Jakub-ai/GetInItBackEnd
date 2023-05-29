using GetInItBackEnd.Entities;

namespace GetInItBackEnd.Models.Account;

public class CreateEmployeeDto
{
    public string Name { get; set; }

    public string LastName { get; set; }
  
    public string Email { get; set; }
 
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public Role Role { get; set; } = Role.EmployeeAccount;
}