/*
using SystemOfWidget.Application.Common.Exceptions;
using SystemOfWidget.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SystemOfWidget.Application.Identity.Account.Commands.SendResetPasswordInvitation
{
    /// <summary>
    /// Команда отправки ссылки на восстановление пароля пользователя
    /// </summary>
    public class SendResetPasswordInvitationCommand : IRequest
    {
        public ForgotPasswordModel Model { get; set; }
    }

    public class SendResetPasswordInvitationCommandHandler : IRequestHandler<SendResetPasswordInvitationCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUrlBuilderService _urlBuilderService;
        private readonly IApplicationEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly HttpContext _httpContext;

        public SendResetPasswordInvitationCommandHandler(UserManager<User> userManager,
            IUrlBuilderService urlBuilderService, IApplicationEmailService emailService, 
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _urlBuilderService = urlBuilderService;
            _emailService = emailService;
            _configuration = configuration;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<Unit> Handle(SendResetPasswordInvitationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Model.Email);
            if (user is null || user.IsDeleted)
                return Unit.Value;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var callbackUrl = _urlBuilderService.ActionWithProtocolFromRequestScheme(action: "ResetPassword",
                controller: "Account", values: new { userId = user.Id, token });
            
#if RELEASE
            var domainOld = _httpContext.Request.Host;
            callbackUrl = callbackUrl.Replace(domainOld.ToString(), _configuration["DomainName"]);
#endif   
            
            var projectName = _configuration["ProjectName"];
            _emailService.Send(request.Model.Email, subject: $"Восстановление пароля в системе \"{projectName}\"",
                message: $@"<p>Уважаемый пользователь!</p>
                    <p>В информационной системе <b>""{projectName}""</b> для Вашей учетной записи был отправлен запрос на восстановление пароля.</p>
                    <p>Для продолжения процедуры восстановления пароля пройдите по <a href='{callbackUrl}'>ссылке</a>.</p>
                    <p>Ссылка станет недействительна либо после завершения процедуры восстановления пароля, либо через 24 часа.</p>",
                isHtml: true);

            return Unit.Value;
        }
    }
}
*/
