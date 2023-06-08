namespace OnlineVoting.Domain.Common
{
    /// <summary>
    /// Базовый объект
    /// </summary>
    public abstract class BaseEntity : IEntityWithId
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }
    }
}
