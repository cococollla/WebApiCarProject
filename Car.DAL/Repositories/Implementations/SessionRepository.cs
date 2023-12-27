using CarWebService.DAL.Models.Entity;
using CarWebService.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CarWebService.DAL.Repositories.Implementations
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ApplicationContext _context;

        public SessionRepository(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавление записи в БД.
        /// </summary>
        /// <param name="sessionData">Данные сессии.</param>
        public async Task<int> CreateSession(Session sessionData)
        {
            await _context.Sessions.AddAsync(sessionData);
            await _context.SaveChangesAsync();

            return sessionData.Id;
        }

        /// <summary>
        /// Удаление записи сессии.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        public async Task<bool> DeleteSesion(int userId)
        {
            var session = await _context.Sessions.FirstOrDefaultAsync(s => s.UserId == userId);

            if (session == null)
            {
                return false;
            }

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Получение записи сессии по идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Данные сессии.</returns>
        public async Task<Session> GetSessionByUserId(int userId)
        {
            return await _context.Sessions.FirstOrDefaultAsync(session => session.UserId == userId);
        }

        /// <summary>
        /// Обновляет данные сессии.
        /// </summary>
        /// <param name="sessionData">Данные для обновления</param>
        public async Task<bool> UpdateSession(Session sessionData)
        {
            var session = await _context.Sessions.FirstOrDefaultAsync(s => s.UserId == sessionData.UserId);

            if (session == null)
            {
                return false;
            }

            session.RefreshToken = sessionData.RefreshToken;
            session.ValidTo = sessionData.ValidTo;

            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Получение рефреш токена.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        public async Task<string> GetRefreshToken(int userId)
        {
            var refreshToken = await _context.Sessions
                .Where(session => session.UserId == userId)
                .Select(session => session.RefreshToken)
                .FirstOrDefaultAsync();

            return refreshToken;
        }
    }
}
