using System.Security.Claims;
using GetInItBackEnd.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace GetInItBackEnd.Authorization;

public class RoleManagerRequirementHandler : AuthorizationHandler<RoleRequirement>
{
    private readonly ILogger<RoleManagerRequirementHandler> _logger;
   

    public RoleManagerRequirementHandler(ILogger<RoleManagerRequirementHandler> logger)
    {
        _logger = logger;
        
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
       
        var role = context.User.FindFirst(c => c.Type == ClaimTypes.Role);
        var name = context.User.FindFirst(c => c.Type == ClaimTypes.Name);
        _logger.LogInformation($"user {name} has role: {role}");

        if (role != null && Enum.TryParse(role.Value, out Role systemRole) && systemRole == Role.ManagerCompanyAccount)
        {
            _logger.LogInformation("Authorization succeed");
            context.Succeed(requirement);
        }
        else
        {
            _logger.LogInformation("Authorization succeed");
        }

        return Task.CompletedTask;
    }
}