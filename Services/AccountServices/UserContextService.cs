using System.Security.Claims;

namespace GetInItBackEnd.Services.AccountServices;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserContextService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public ClaimsPrincipal? User => _contextAccessor.HttpContext?.User;

    public int? GetUserId =>
        User is null ? null : (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    public int? GetCompanyId =>
        User is null ? null : (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
}