using GetInItBackEnd.Entities;
using Microsoft.AspNetCore.Authorization;

namespace GetInItBackEnd.Authorization;

public class RoleRequirement : IAuthorizationRequirement
{
    public Role Role { get; }

    public RoleRequirement(Role role)
    {
        Role = role;
    }
}