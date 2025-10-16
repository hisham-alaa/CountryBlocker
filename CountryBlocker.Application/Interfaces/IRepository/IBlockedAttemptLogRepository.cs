using CountryBlocker.Domain.Models;

namespace CountryBlocker.Application.Interfaces.IRepository
{
    public interface IBlockedAttemptLogRepository
    {
        Task AddAsync(BlockedAttemptLog log);
        Task<IEnumerable<BlockedAttemptLog>> GetPagedAsync(int page, int pageSize);
    }
}