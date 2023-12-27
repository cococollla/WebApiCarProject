namespace CarWebService.API.Models
{
    /// <summary>
    /// Модель для входа.
    /// </summary>
    public class AuthRequest
    {
        /// <summary>
        /// Почта пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; }
    }
}