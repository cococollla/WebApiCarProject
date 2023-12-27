namespace CarWebService.BLL.Services.Contracts
{
    public interface ITokenServices
    {
        /// <summary>
        /// Создание access токена.
        /// </summary>
        /// <param name="role">Роль пользователя.</param>
        /// <param name="email">Электронный адрес пользователяю</param>
        public string CreateToken(string role, string email);

        /// <summary>
        /// Создание refresh токена.
        /// </summary>
        /// <param name="role">Роль пользователя.</param>
        /// <param name="email">Электронная почта пользователя.</param>
        public string CreateRefreshToken(string role, string email);
    }
}
