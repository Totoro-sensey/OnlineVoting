namespace OnlineVoting.Application.Account.Commands.RefreshToken
{
    /// <summary>
    /// ViewModel данных пользователя с токенами для команды обновления токенов
    /// </summary>
    public class RefreshTokenVm
    {
        /// <summary>
        /// Токен доступа
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Токен обновления
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Фамилия Имя Отчество
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Электронный адрес
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Список наименований ролей
        /// </summary>
        public IEnumerable<string> Roles { get; set; }
    }
}
