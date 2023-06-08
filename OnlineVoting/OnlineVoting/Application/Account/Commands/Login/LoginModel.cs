namespace SystemOfWidget.Application.Identity.Account.Commands.Login
{
    /// <summary>
    /// Dto данных пользователя для команды входа в систему
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Электронный адрес
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Цифровой отпечок устройства пользователя
        /// </summary>
        public string Fingerprint { get; set; }
    }
}
