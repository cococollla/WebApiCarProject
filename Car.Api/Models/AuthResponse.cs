namespace CarWebService.API.Models
{
    /// <summary>
    /// Модель для ответа при успешной авторизации.
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// Роль авторизованного пользователя.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Токен пользователя.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }
    }
}