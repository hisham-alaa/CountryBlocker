using CountryBlocker.Application.Interfaces.IRepository;
using CountryBlocker.Domain.Models;
using System.Collections.Concurrent;

namespace CountryBlocker.Infrastructure.Repositories
{
    public class InMemoryLogRepository : IBlockedAttemptLogRepository
    {
        private readonly ConcurrentQueue<BlockedAttemptLog> _logs = new();

        public Task AddAsync(BlockedAttemptLog log)
        {
            _logs.Enqueue(log);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<BlockedAttemptLog>> GetPagedAsync(int page, int pageSize)
        {
            var allLogs = _logs.Reverse().ToList(); // Reverse to show newest first
            var paged = allLogs
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult<IEnumerable<BlockedAttemptLog>>(paged);
        }
    }
}