namespace CarWebService.BLL.Services.Contracts
{
    public interface ITokenServices
    {
        /// <summary>
        /// Создание access token.
        /// </summary>
        /// <param name="role">Роль пользователя.</param>
        /// <returns>Access token.</returns>
        public string CreateToken(string role);

        /// <summary>
        /// Создание rafresh token.
        /// </summary>
        public string CreateRefreshToken();
    }
}
