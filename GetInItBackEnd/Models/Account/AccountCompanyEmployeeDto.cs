using System.Text.Json.Serialization;
using GetInItBackEnd.Entities;

namespace GetInItBackEnd.Models.Account;

public class AccountCompanyEmployeeDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Role Role { get; set; }
}