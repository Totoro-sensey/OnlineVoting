using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using OnlineVoting.Models.Identity.Entities;

namespace OnlineVoting.Application.Account.Commands.VerifyToken
{
    /// <summary>
    /// Команда проверки срока действия токена сброса пароля пользователя
    /// </summary>
    public class VerifyTokenCommand : IRequest
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }

    public class VerifyTokenHandler : IRequestHandler<VerifyTokenCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public VerifyTokenHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(VerifyTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
                throw new NullReferenceException(message: "Страница не найдена");

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var isValidToken = await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider,
                "ResetPassword", token);
            if (!isValidToken)
                throw new NullReferenceException(message: "Страница не найдена");

            return Unit.Value;
        }
    }
}
