using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineVoting.Application.Services.Abstracts;
using OnlineVoting.Models;
using OnlineVoting.Models.Identity.Entities;

namespace OnlineVoting.Application.Account.Commands.RefreshToken
{
    /// <summary>
    /// Команда обновления пары токенов: AccessToken и RefreshToken
    /// </summary>
    public class RefreshTokenCommand : IRequest<RefreshTokenVm>
    {
        public string RefreshToken { get; set; }
        public string Fingerprint { get; set; }
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenVm>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IIdentityTokenService _identityTokenService;
        private readonly UserManager<ApplicationUser> _userManager;

        public RefreshTokenCommandHandler(IApplicationDbContext dbContext, IIdentityTokenService identityTokenService, 
            UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _identityTokenService = identityTokenService;
            _userManager = userManager;
        }

        public async Task<RefreshTokenVm> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshTokenHash = _identityTokenService.GetHashForAuthData(request.RefreshToken);
            var fingerprintHash = _identityTokenService.GetHashForAuthData(request.Fingerprint);

            var user = await GetUserByRefreshToken(refreshTokenHash, cancellationToken);

            var oldRefreshSession = user.RefreshSessions
                .Single(i => i.TokenHash == refreshTokenHash);

            _dbContext.RefreshSessions.Remove(oldRefreshSession);
            await _dbContext.SaveChangesAsync();

            if (_identityTokenService.IsExpired(oldRefreshSession) 
                || oldRefreshSession.FingerprintHash != fingerprintHash)
                throw new NullReferenceException();

            var (accessToken, refreshToken) = await _identityTokenService.GenerateTokensAsync(user, 
                request.Fingerprint);

            await _userManager.UpdateAsync(user);

            return new RefreshTokenVm
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserName = user.UserName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user),
            };
        }

        private async Task<ApplicationUser> GetUserByRefreshToken(string tokenHash, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Include(i => i.RefreshSessions)
                .Where(i => i.RefreshSessions.Any(j => j.TokenHash == tokenHash))
                .SingleOrDefaultAsync(cancellationToken);
            if (user is null)
                throw new NullReferenceException();

            return user;
        }
    }
}
