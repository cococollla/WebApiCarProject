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

        public async Task<int> CreateSession(Session sessionData)
        {
            var sessionId = await _sessionRepository.CreateSession(sessionData);

            return sessionId;
        }

        public async Task<bool> DeleteSesion(int userId)
        {
            var result = await _sessionRepository.DeleteSesion(userId);

            return result;
        }

        public async Task<Session> GetSessionByUserId(int userId)
        {
            var session = await _sessionRepository.GetSessionByUserId(userId);

            return session;
        }

        public async Task<bool> UpdateSession(Session sessionData)
        {
            var result = await _sessionRepository.UpdateSession(sessionData);

            return result;
        }

        public async Task<string> GetRefreshToken(int userId)
        {
            var refreshToken = await _sessionRepository.GetRefreshToken(userId);

            return refreshToken;
        }
    }
}
