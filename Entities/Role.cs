using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GetInItBackEnd.Entities;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    [EnumMember(Value = "Admin")]
    Admin = 1,
    [EnumMember(Value = "Manager")]
    ManagerCompanyAccount = 2,
    [EnumMember(Value = "Employee")]
    EmployeeAccount = 3,
    [EnumMember(Value = "User")]
    UserAccount = 4 
}