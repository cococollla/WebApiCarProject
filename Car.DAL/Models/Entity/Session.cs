using System.ComponentModel.DataAnnotations;

namespace CarWebService.DAL.Models.Entity
{
    /// <summary>
    /// Модель сессии.
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Рефреш токен.
        /// </summary>
        [Required]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Дата истечения времени жизни токена.
        /// </summary>
        [Required]
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// Внешний ключ - идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Ссылка на таблицу пользователей.
        /// </summary>
        public User User { get; set; }
    }
}