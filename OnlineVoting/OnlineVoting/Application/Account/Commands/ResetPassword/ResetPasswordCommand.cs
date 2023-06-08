using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using OnlineVoting.Models.Identity.Entities;
using SystemOfWidget.Application.Common.Exceptions;

namespace OnlineVoting.Application.Account.Commands.ResetPassword
{
    /// <summary>
    /// Команда смены пароля пользователя
    /// </summary>
    public class ResetPasswordCommand : IRequest
    {
        public ResetPasswordModel Model { get; set; }
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Model.UserId);
            if (user is null)
                return Unit.Value;

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Model.Token));
            var result = await _userManager.ResetPasswordAsync(user, token, request.Model.Password);
            if (!result.Succeeded)
                throw new BadRequestException("Не удалось сменить пароль");

            return Unit.Value;
        }
    }
}
