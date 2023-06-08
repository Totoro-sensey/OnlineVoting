using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using OnlineVoting.Application.Services.Abstracts;
using OnlineVoting.Domain.Models.Identity.Entities;
using OnlineVoting.Models.Identity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using OnlineVoting.Infrastructure.Models;


namespace OnlineVoting.Infrastructure.Services
{
    public class IdentityTokenService : IIdentityTokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDateTimeService _dateTimeService;
        private readonly JwtOptions _jwtOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityTokenService(UserManager<ApplicationUser> userManager, IDateTimeService dateTimeService,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _dateTimeService = dateTimeService;
            _httpContextAccessor = httpContextAccessor;
            _jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>();
        }

        public async Task<(string accessToken, string refreshToken)> GenerateTokensAsync(ApplicationUser applicationUser, string fingerprint)
        {
            var accessToken = await GenerateAccessToken(applicationUser);
            var (refreshToken, refreshSession) = GenerateRefreshSession(fingerprint);
            applicationUser.RefreshSessions.Add(refreshSession);
            RemoveOldRefreshTokens(applicationUser);

            return (accessToken, refreshToken);
        }

        private async Task<string> GenerateAccessToken(ApplicationUser applicationUser)
        {
            var claims = await GetClaimsForAccessToken(applicationUser);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = _dateTimeService.UtcNow.AddMinutes(_jwtOptions.TokenExpiresMinutes),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);

            _httpContextAccessor.HttpContext.Session.SetString("AccessToken", result);

            return result;
        }

        private async Task<IEnumerable<Claim>> GetClaimsForAccessToken(ApplicationUser applicationUser)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, applicationUser.UserName),
                new(ClaimTypes.NameIdentifier, applicationUser.Id),
                new(ClaimTypes.Email, applicationUser.Email)
            };

            var roles = await _userManager.GetRolesAsync(applicationUser);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }

        private (string token, RefreshSession session) GenerateRefreshSession(string fingerprint)
        {
            var token = Guid.NewGuid().ToString();

            var session = new RefreshSession
            {
                TokenHash = GetHashForAuthData(token),
                FingerprintHash = GetHashForAuthData(fingerprint),
                CreatedAt = _dateTimeService.UtcNow,
                ExpiresIn = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiresDays),
                UserAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString(),
                IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
            };

            return (token, session);
        }

        private int RemoveOldRefreshTokens(ApplicationUser applicationUser)
            => applicationUser.RefreshSessions.RemoveAll(i => IsExpired(i)
                                                   && i.CreatedAt.AddDays(_jwtOptions.RefreshTokenTtl) <= _dateTimeService.UtcNow);

        public bool IsExpired(RefreshSession token)
            => token.ExpiresIn <= _dateTimeService.UtcNow;

        public string GetHashForAuthData(string data)
        {
            using var hashAlgorithm = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(hashAlgorithm.ComputeHash(bytes));
        }

        public void SetRefreshTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = _dateTimeService.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiresDays)
            };
            _httpContextAccessor.HttpContext.Response.Cookies
                .Append(_jwtOptions.RefreshTokenCookieName, token, cookieOptions);
        }

        public void DeleteRefreshTokenCookie()
        {
            _httpContextAccessor.HttpContext.Session.Remove("AccessToken");
            _httpContextAccessor.HttpContext.Response.Cookies
                    .Delete(_jwtOptions.RefreshTokenCookieName);
        }

        public string GetRefreshTokenFromCookie()
        {
            var cookies = _httpContextAccessor.HttpContext.Request.Cookies;
            if (!cookies.ContainsKey(_jwtOptions.RefreshTokenCookieName))
                throw new NullReferenceException();

            return cookies[_jwtOptions.RefreshTokenCookieName];
        }
    }
}
