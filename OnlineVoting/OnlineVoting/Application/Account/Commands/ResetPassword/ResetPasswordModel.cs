using System.ComponentModel.DataAnnotations;

namespace OnlineVoting.Application.Account.Commands.ResetPassword
{
    /// <summary>
    /// Dto с новым паролем пользователя для команды смены пароля
    /// </summary>
    public class ResetPasswordModel
    {
        /// <summary>
        /// Новый пароль
        /// </summary>
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Повтор нового пароля
        /// </summary>
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Токен смены пароля
        /// </summary>
        public string Token { get; set; }
    }
}
