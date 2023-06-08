using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace OnlineVoting.Domain.Identity.Enums
{
    /// <summary>
    /// Роль
    /// </summary>
    public static class RoleName
    {
        /// <summary>
        /// Полный доступ
        /// </summary>
        public const string Root = "root";

        /// <summary>
        /// Технический администратор
        /// </summary>
        public const string TechnicalAdmin = "technicalAdmin";

        /// <summary>
        /// Системный администратор
        /// </summary>
        public const string SystemAdmin = "systemAdmin";

        /// <summary>
        /// Доступ только на чтение
        /// </summary>
        public const string AllReadOnly = "allReadOnly";


        /// <summary>
        /// Метод получения списка наименований ролей
        /// </summary>
        /// <returns></returns>
        public static IList<string> GetList()
        {
            var type = typeof(RoleName);

            return type.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(i => i.GetValue(type)?.ToString())
                .ToList();
        }

        /// <summary>
        /// Метод получения списка кортежей ролей: (наименование, значение)
        /// </summary>
        /// <returns></returns>
        public static IList<(string Name, string Value)> GetListWithNames()
        {
            var type = typeof(RoleName);

            return type.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(i =>
                (
                    (i.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute)?.Name,
                    i.GetValue(type)?.ToString()
                ))
                .ToList();
        }
    }
}
