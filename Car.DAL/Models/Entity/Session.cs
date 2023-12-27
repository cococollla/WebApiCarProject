using System.ComponentModel.DataAnnotations;

namespace CarWebService.DAL.Models.Entity
{
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
        /// Внешний ключ. иеднтификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Ссылка на таблиц пользователей.
        /// </summary>
        public User User { get; set; }
    }
}