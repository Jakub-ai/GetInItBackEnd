using GetInItBackEnd.Entities;

namespace GetInItBackEnd.Models;

public class AccountDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public Company? Company { get; set; }
    
}