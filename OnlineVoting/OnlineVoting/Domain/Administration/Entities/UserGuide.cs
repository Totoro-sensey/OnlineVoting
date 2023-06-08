using OnlineVoting.Domain.Common;
using SystemOfWidget.Domain.Identity.Entities;

namespace OnlineVoting.Domain.Administration.Entities
{
    /// <summary>
    /// Руководство пользователя
    /// </summary>
    [Auditable]
    public class UserGuide : BaseEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата последнего обновления
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Ссылка на документ
        /// </summary>
        public long? DocumentId { get; set; }

        /// <summary>
        /// Роли
        /// </summary>
        public List<ApplicationRole> Roles { get; set; } = new();
    }
}
