using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace GetInItBackend.Integration.Tests;

public class FakePolicyManagerEvaluator : IPolicyEvaluator
{
    public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
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
        var ticket = new AuthenticationTicket(claimsPrinciple, "test");
        var result = AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }

    public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context,
        object? resource)
    {
        var result = PolicyAuthorizationResult.Success();
        return Task.FromResult(result);
    }
}