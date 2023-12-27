using CarWebService.BLL.Services.Contracts;
using CarWebService.DAL.Models.Entity;
using CarWebService.DAL.Repositories.Contracts;

namespace CarWebService.BLL.Services.Implementations
{
    public class SessionServices : ISessionServices
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionServices(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        /// <summary>
        /// Добавление записи в БД.
        /// </summary>
        /// <param name="sessionData">Данные сессии.</param>
        public async Task<int> CreateSession(Session sessionData)
        {
            var sessionId = await _sessionRepository.CreateSession(sessionData);

            return sessionId;
        }

        /// <summary>
        /// Удаление записи сессии.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        public async Task<bool> DeleteSesion(int userId)
        {
            var result = await _sessionRepository.DeleteSesion(userId);

            return result;
        }

        /// <summary>
        /// Получение записи сессии по идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Данные сессии.</returns>
        public async Task<Session> GetSessionByUserId(int userId)
        {
            var session = await _sessionRepository.GetSessionByUserId(userId);

            return session;
        }

        /// <summary>
        /// Обновляет данные сессии.
        /// </summary>
        /// <param name="sessionData">Данные для обновления</param>
        public async Task<bool> UpdateSession(Session sessionData)
        {
            var result = await _sessionRepository.UpdateSession(sessionData);

            return result;
        }

        /// <summary>
        /// Получение рефреш токена.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        public async Task<string> GetRefreshToken(int userId)
        {
            var refreshToken = await _sessionRepository.GetRefreshToken(userId);

            return refreshToken;
        }
    }
}
