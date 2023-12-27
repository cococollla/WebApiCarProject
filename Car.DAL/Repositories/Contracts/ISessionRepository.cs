using CarWebService.DAL.Models.Entity;

namespace CarWebService.DAL.Repositories.Contracts
{
    public interface ISessionRepository
    {
        /// <summary>
        /// Добавление записи в БД.
        /// </summary>
        /// <param name="sessionData">Данные сессии.</param>
        public Task<int> CreateSession(Session sessionData);

        /// <summary>
        /// Обновляет данные сессии.
        /// </summary>
        /// <param name="sessionData">Данные для обновления</param>
        public Task<bool> UpdateSession(Session sessionData);

        /// <summary>
        /// Получение записи сессии по идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Данные сессии.</returns>
        public Task<Session> GetSessionByUserId(int userId);

        /// <summary>
        /// Удаление записи сессии.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        public Task<bool> DeleteSesion(int userId);

        /// <summary>
        /// Получение рефреш токена.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        public Task<string> GetRefreshToken(int userId);
    }
}