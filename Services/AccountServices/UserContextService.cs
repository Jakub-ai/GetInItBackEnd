using System.Security.Claims;
using GetInItBackEnd.Entities;

namespace GetInItBackEnd.Services.AccountServices
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public ClaimsPrincipal? User => _contextAccessor.HttpContext?.User;

        public int? GetUserId =>
            User is null ? null : (int?)ParseClaimToInt(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        
        public int? GetCompanyId =>
            User is null ? null : (int?)ParseClaimToInt(User.FindFirst(c => c.Type == ClaimTypes.Actor)?.Value);

        public string? GetUserName => User is null ? null : User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
        public string? GetUserRole => User is null ? null : User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
        public string? GetUserLastName => User is null ? null : User.FindFirst(c => c.Type == ClaimTypes.Surname)?.Value;
        public string? GetUserMail => User is null ? null : User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

        private int? ParseClaimToInt(string? claimValue)
        {
            if (int.TryParse(claimValue, out int parsedValue))
            {
                return parsedValue;
            }
            
            return null;
        }
    }
}