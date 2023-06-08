using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineVoting.Application.Account.Commands.Login;
using OnlineVoting.Application.Account.Commands.Logout;
using OnlineVoting.Application.Account.Commands.RefreshToken;
using OnlineVoting.Application.Services.Abstracts;
using OnlineVoting.Controllers.Common;
using OnlineVoting.Models.Identity.Entities;

namespace OnlineVoting.WebUI.Controllers.Identity
{
    /// <summary>
    /// Контроллер для работы с учетной записью пользователя
    /// </summary>
    [Authorize]
    public class AccountController : MediatorController
    {
        /// <summary>
        /// ViewModel данных пользователя после создания/обновления токенов
        /// </summary>
        /// <param name="AccessToken">Токен доступа</param>
        /// <param name="UserName">Фамилия Имя Отчество</param>
        /// <param name="Email">Электронный адрес</param>
        /// <param name="Roles">Роли</param>
        public record AuthData(string AccessToken, string UserName, string Email, IEnumerable<string> Roles);

        /// <summary>
        /// Dto токена обновления
        /// </summary>
        /// <param name="Fingerprint">Цифровой отпечаток устройства пользователя</param>
        public record RefreshTokenDto(string Fingerprint);

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityTokenService _identityTokenService;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager, IIdentityTokenService identityTokenService, 
            IConfiguration configuration)
        {
            _userManager = userManager;
            _identityTokenService = identityTokenService;
            _configuration = configuration;
        }

        /// <summary>
        /// Метод входа в систему
        /// </summary>
        /// <param name="command">Команда входа в систему 
        /// с объектом Login (авторизационные данные и fingerprint) в теле запроса</param>
        /// <returns>Возвращает данные о пользователи и AccessToken в теле ответа, RefreshToken в куках</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<AuthData> Login([FromBody] LoginCommand command)
        {
            var result = await Mediator.Send(command);
            _identityTokenService.SetRefreshTokenCookie(result.RefreshToken);

            return new AuthData(result.AccessToken, result.UserName, result.Email, result.Roles);
        }

        /// <summary>
        /// Метод обновления пары токенов: AccessToken и RefreshToken
        /// </summary>
        /// <param name="dto">Объект с fingerprint в теле запроса</param>
        /// <returns>Возвращает данные о пользователе и новый AccessToken в теле ответа, 
        /// новый RefreshToken в куках</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<AuthData> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            var result = await Mediator.Send(new RefreshTokenCommand
            {
                RefreshToken = _identityTokenService.GetRefreshTokenFromCookie(),
                Fingerprint = dto.Fingerprint
            });
            _identityTokenService.SetRefreshTokenCookie(result.RefreshToken);

            return new AuthData(result.AccessToken, result.UserName, result.Email, result.Roles);
        }

        /// <summary>
        /// Метод выхода из системы
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task Logout()
        {
            await Mediator.Send(new LogoutCommand
            {
                RefreshToken = _identityTokenService.GetRefreshTokenFromCookie()
            });
            _identityTokenService.DeleteRefreshTokenCookie();
        }

        /*
        /// <summary>
        /// Метод отправки ссылки на восстановление пароля
        /// </summary>
        /// <param name="model">Модель с электронным адресом пользователя</param>
        /// <returns>Возвращает страницу с подтверждением отправки ссылки на восстановление пароля</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            ViewBag.Project = _configuration.GetValue<string>("ProjectName");

            var command = new SendResetPasswordInvitationCommand() { Model = model };
            try
            {
                await Mediator.Send(command);
                return View("ForgotPasswordConfirmation");
            }
            catch
            {
                ModelState.AddModelError("", "Не удалось отправить ссылку для восстановления пароля");
                return View(model);
            }
        }
        */
    }
}
