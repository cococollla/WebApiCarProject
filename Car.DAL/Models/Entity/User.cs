using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarWebService.DAL.Models.Entity
{
    /// <summary>
    /// Модель ползователя.
    /// </summary>
    public class User : IdentityUser<int>
    {
        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public Role Role { get; set; }
    }
}
