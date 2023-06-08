using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineVoting.Application.Services.Abstracts;
using OnlineVoting.Domain.Administration.Entities;
using OnlineVoting.Models;
using OnlineVoting.Models.Identity.Entities;
using SystemOfWidget.Application.Common.Exceptions;
using SystemOfWidget.Application.Identity.Account.Commands.Login;

namespace OnlineVoting.Application.Account.Commands.Login
{
    /// <summary>
    /// Команда входа в систему
    /// </summary>
    public class LoginCommand : IRequest<LoginVm>
    {
        public LoginModel Login { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginVm>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityTokenService _identityTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginCommandHandler(IApplicationDbContext dbContext, SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager, IIdentityTokenService identityTokenService,
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _identityTokenService = identityTokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginVm> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await GetUser(request.Login.Email, cancellationToken);
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Login.Password,
                lockoutOnFailure: true);
            if (result.IsLockedOut)
                throw new BadRequestException("Превышено количество попыток авторизации. Попробуйте авторизоваться позже.");
            if (!result.Succeeded)
                throw new BadRequestException("Неверный адрес электронной почты или пароль");

            await CheckUserSessions(user, fingerprint: request.Login.Fingerprint);
            var (accessToken, refreshToken) = await _identityTokenService.GenerateTokensAsync(user,
                request.Login.Fingerprint);

            await _userManager.UpdateAsync(user);
            await CreateLoginMark(user);

            return new LoginVm
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserName = user.UserName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user),
            };
        }

        /// <summary>
        /// Метод получения пользователя системы по электронному адресу
        /// </summary>
        /// <param name="email">Адрес электронной почты</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Возвращает пользователя системы</returns>
        /// <exception cref="BadRequestException">Ошибка получения пользователя системы</exception>
        private async Task<ApplicationUser> GetUser(string email, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Include(i => i.RefreshSessions)
                .SingleOrDefaultAsync(i => i.Email == email, cancellationToken);
            if (user is null)
                throw new BadRequestException("Неверный адрес электронной почты или пароль");

            return user;
        }

        /// <summary>
        /// Метод проверки максимального числа сессий пользователя системы
        /// </summary>
        /// <param name="applicationUser">Пользователь системы</param>
        /// <param name="fingerprint">Цифровой отпечаток устройства пользователя</param>
        /// <returns></returns>
        async private Task<Unit> CheckUserSessions(ApplicationUser applicationUser, string fingerprint)
        {
            const int maxRefreshSessionsCount = 3;

            var fingerprintHash = _identityTokenService.GetHashForAuthData(fingerprint);
            var oldRefreshSession = applicationUser.RefreshSessions.FirstOrDefault(i => i.FingerprintHash == fingerprintHash);

            if (oldRefreshSession is null && applicationUser.RefreshSessions.Count >= maxRefreshSessionsCount)
            {
                var badRefreshSessions = applicationUser.RefreshSessions.OrderByDescending(i => i.CreatedAt).Skip(1);
                _dbContext.RefreshSessions.RemoveRange(badRefreshSessions);
            } 
            else if (oldRefreshSession is not null)
            {
                _dbContext.RefreshSessions.Remove(oldRefreshSession);
            }

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        /// <summary>
        /// Метод создания отметки о входе пользователя в систему
        /// </summary>
        /// <param name="applicationUser">Пользователь системы</param>
        /// <returns></returns>
        async private Task<Unit> CreateLoginMark(ApplicationUser applicationUser)
        {
            var loginMark = new LoginMark()
            {
                LoggedAt = DateTime.Now,
                UserAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString(),
                IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                ApplicationUser = applicationUser
            };
            _dbContext.LoginMarks.Add(loginMark);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
