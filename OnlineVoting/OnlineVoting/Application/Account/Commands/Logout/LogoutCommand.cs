using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineVoting.Application.Services.Abstracts;
using OnlineVoting.Domain.Models.Identity.Entities;
using OnlineVoting.Models;
using SystemOfWidget.Application.Common.Exceptions;

namespace OnlineVoting.Application.Account.Commands.Logout
{
    /// <summary>
    /// Команда выхода из системы
    /// </summary>
    public class LogoutCommand : IRequest
    {
        public string RefreshToken { get; set; }
    }

    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IIdentityTokenService _identityTokenService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public LogoutCommandHandler(IApplicationDbContext dbContext, IIdentityTokenService identityTokenService,
            ICurrentUserService currentUserService, IDateTimeService dateTimeService)
        {
            _dbContext = dbContext;
            _identityTokenService = identityTokenService;
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
        }


        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var refreshTokenHash = _identityTokenService.GetHashForAuthData(request.RefreshToken);

            var refreshSession = await GetRefreshSession(refreshTokenHash, cancellationToken);
            _dbContext.RefreshSessions.Remove(refreshSession);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<RefreshSession> GetRefreshSession(string tokenHash, CancellationToken cancellationToken)
        {
            var refreshSession = await _dbContext.RefreshSessions
                .FirstOrDefaultAsync(i => i.TokenHash == tokenHash
                                     && i.UserId == _currentUserService.UserId, cancellationToken);
            if (refreshSession is null || _identityTokenService.IsExpired(refreshSession))
                throw new BadRequestException("Не удалось выполнить выход из системы");

            return refreshSession;
        }
    }
}
