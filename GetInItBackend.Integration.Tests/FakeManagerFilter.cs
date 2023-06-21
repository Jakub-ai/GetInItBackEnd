using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GetInItBackend.Integration.Tests;

public class FakeManagerFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var claimsPrinciple = new ClaimsPrincipal();
        claimsPrinciple.AddIdentity(new ClaimsIdentity(new []
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Actor, "1"),
            new Claim(ClaimTypes.Name, "Name"),
            new Claim(ClaimTypes.Role, "ManagerCompanyAccount" ),
            new Claim(ClaimTypes.Surname, "LastName"),
            new Claim(ClaimTypes.Email, $"Email@email.com")
        }));
      
        context.HttpContext.User = claimsPrinciple;

        await next();
    }
}