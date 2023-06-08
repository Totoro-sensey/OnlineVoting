using OnlineVoting.Domain.Common;
using OnlineVoting.Models.Identity.Entities;

namespace OnlineVoting.Domain.Administration.Entities
{
    /// <summary>
    /// Отметка о входе пользователя в систему
    /// </summary>
    [Auditable]
    public class LoginMark : BaseEntity
    {
        /// <summary>
        /// Дата и время входа в систему
        /// </summary>
        public DateTime LoggedAt { get; set; }

        /// <summary>
        /// Устройство пользователя
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Ip адрес пользователя
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }


        // Навигационные свойства
        public ApplicationUser ApplicationUser{ get; set; }
    }
}
