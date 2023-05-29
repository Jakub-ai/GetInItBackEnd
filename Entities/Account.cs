using System.Text.Json.Serialization;

namespace GetInItBackEnd.Entities;

public class Account
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public int? CreatedById { get; set; }
    public virtual Account? CreatedBy { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Role Role { get; set; } = Role.ManagerCompanyAccount;
    public int? CompanyId { get; set; }
    public  Company? Company { get; set; }
    
    
}