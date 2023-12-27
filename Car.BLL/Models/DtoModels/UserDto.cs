using System.ComponentModel.DataAnnotations;

namespace CarWebService.BLL.Models.DtoModels
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Почта пользоавтеля.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public string? RoleName { get; set; }
    }
}