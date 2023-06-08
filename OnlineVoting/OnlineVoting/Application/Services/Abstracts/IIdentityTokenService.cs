using OnlineVoting.Domain.Models.Identity.Entities;
using OnlineVoting.Models.Identity.Entities;

namespace OnlineVoting.Application.Services.Abstracts
{
    public interface IIdentityTokenService
    {
        Task<(string accessToken, string refreshToken)> GenerateTokensAsync(ApplicationUser applicationUser, string fingerprint);
        string GetHashForAuthData(string data);
        void SetRefreshTokenCookie(string token);
        void DeleteRefreshTokenCookie();
        string GetRefreshTokenFromCookie();
        bool IsExpired(RefreshSession token);
    }
}
