namespace OnlineVoting.Application.Account.Commands.SendResetPasswordInvitation
{
    /// <summary>
    /// ViewModel с электронным адресом пользователя для команды отправки ссылки на восстановление пароля
    /// </summary>
    public class ForgotPasswordModel
    {
        /// <summary>
        /// Электронный адрес
        /// </summary>
        public string Email { get; set; }
    }
}
