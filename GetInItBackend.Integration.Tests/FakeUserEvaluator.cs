using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GetInItBackend.Integration.Tests;

public class FakeUserEvaluator : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var claimsPrincipleUser = new ClaimsPrincipal();
        claimsPrincipleUser.AddIdentity(new ClaimsIdentity(new []
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Actor, "1"),
            new Claim(ClaimTypes.Name, "Name"),
            new Claim(ClaimTypes.Role, "UserAccount" ),
            new Claim(ClaimTypes.Surname, "LastName"),
            new Claim(ClaimTypes.Email, $"Email@email.com")
        }));
        context.HttpContext.User = claimsPrincipleUser;

        await next();
    }
}