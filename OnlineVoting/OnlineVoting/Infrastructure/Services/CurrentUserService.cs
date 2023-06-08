using System.Security.Claims;
using OnlineVoting.Application.Services.Abstracts;

namespace OnlineVoting.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        public string Email => _httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.Email);

        public IEnumerable<string> Roles => _httpContextAccessor.HttpContext?.User
            .FindAll(ClaimTypes.Role)
            .Select(i => i.Value);
    }
}
