namespace OnlineVoting.Domain.Common
{
    /// <summary>
    /// Файл
    /// </summary>
    public class FileEntity : BaseEntity
    {
        /// <summary>
        /// Наименование файла
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Размер файла
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// Содержание файла
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Тип файла
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Дата загрузки файла
        /// </summary>
        public DateTime UploadDate { get; set; }
    }
}
