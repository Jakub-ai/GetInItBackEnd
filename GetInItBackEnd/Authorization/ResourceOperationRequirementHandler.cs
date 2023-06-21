using GetInItBackEnd.Entities;
using Microsoft.AspNetCore.Authorization;

namespace GetInItBackEnd.Authorization;

public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Account>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement,
        Account resource)
    {
        if (requirement.ResourceOperation == ResourceOperation.Read ||
            requirement.ResourceOperation == ResourceOperation.Create || requirement.ResourceOperation == ResourceOperation.Update)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}