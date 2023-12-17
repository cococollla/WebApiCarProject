namespace CarWebService.API.Models
{
    /// <summary>
    /// Моделья для ответа прни успешной авторизации
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// Роль авторизованного пользователя
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Токен пользователя
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Рефреш токен
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
