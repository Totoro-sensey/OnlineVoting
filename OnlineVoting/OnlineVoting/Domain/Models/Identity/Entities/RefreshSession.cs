using OnlineVoting.Domain.Common;
using OnlineVoting.Models.Identity.Entities;

namespace OnlineVoting.Domain.Models.Identity.Entities
{
    /// <summary>
    /// Сессия для обновления JWT
    /// </summary>
    public class RefreshSession : BaseEntity
    {
        /// <summary>
        /// Хэш токена обновления (RefreshToken)
        /// </summary>
        public string TokenHash { get; set; }

        /// <summary>
        /// Хэш цифрового отпечатка устройства пользователя
        /// </summary>
        public string FingerprintHash { get; set; }

        /// <summary>
        /// Дата и время создания токена обновления (RefreshToken)
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата и время истечения срока действия токена обновления (RefreshToken)
        /// </summary>
        public DateTime ExpiresIn { get; set; }

        /// <summary>
        /// Устройство пользователя
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Ip адрес
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }

        // Навигационные свойства
        public ApplicationUser ApplicationUser { get; set; }
    }
}

