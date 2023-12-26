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

        public async Task<int> CreateSession(Session sessionData)
        {
            await _context.Sessions.AddAsync(sessionData);
            await _context.SaveChangesAsync();

            return sessionData.Id;
        }

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

        public async Task<Session> GetSessionByUserId(int userId)
        {
            return await _context.Sessions.FirstOrDefaultAsync(session => session.UserId == userId);
        }

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
