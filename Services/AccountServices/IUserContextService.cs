using System.Security.Claims;

namespace GetInItBackEnd.Services.AccountServices;

public interface IUserContextService
{
    ClaimsPrincipal? User { get; }
    int? GetUserId { get; }
    public int? GetCompanyId { get; }
}