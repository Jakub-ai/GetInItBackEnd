using Microsoft.AspNetCore.Authorization;

namespace GetInItBackEnd.Authorization;

public enum ResourceOperation
{
    Create,
    Read,
    Delete,
    Update
}
public class ResourceOperationRequirement : IAuthorizationRequirement
{
    public ResourceOperation ResourceOperation { get; }

    public ResourceOperationRequirement(ResourceOperation resourceOperation)
    {
        ResourceOperation = resourceOperation;
    }
}