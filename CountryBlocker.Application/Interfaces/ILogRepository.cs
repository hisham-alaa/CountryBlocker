using CountryBlocker.Domain.Models;

namespace CountryBlocker.Domain.Interfaces
{
    public interface ILogRepository
    {
        void Add(BlockAttemptLog log);

        IEnumerable<BlockAttemptLog> GetAll();
    }
}