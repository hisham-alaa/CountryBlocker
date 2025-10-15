using CountryBlocker.Domain.Interfaces;
using CountryBlocker.Domain.Models;

namespace CountryBlocker.Application.Services
{
    public class LogService
    {
        private readonly ILogRepository _logRepo;

        public LogService(ILogRepository logRepo)
        {
            _logRepo = logRepo;
        }

        public IEnumerable<BlockAttemptLog> GetLogs()
        {
            return _logRepo.GetAll();
        }
    }
}