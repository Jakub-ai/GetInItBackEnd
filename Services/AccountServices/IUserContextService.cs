using System.Security.Claims;

namespace GetInItBackEnd.Services.AccountServices;

public interface IUserContextService
{
    ClaimsPrincipal? User { get; }
    int? GetUserId { get; }
    public int? GetCompanyId { get; }
    public string? GetUserName { get; }
    public string? GetUserRole { get; }
    public string? GetUserLastName { get; }
    public string? GetUserCompanyName { get; }
    public string? GetUserMail { get; }
}