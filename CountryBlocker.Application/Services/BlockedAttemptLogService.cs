using CountryBlocker.Application.DTOs;
using CountryBlocker.Application.Interfaces.IRepository;
using CountryBlocker.Application.Interfaces.IService;
using CountryBlocker.Domain.Models;

namespace CountryBlocker.Application.Services
{
    public class BlockedAttemptLogService : IBlockedAttemptLogService
    {
        private readonly IBlockedAttemptLogRepository _logRepo;

        public BlockedAttemptLogService(IBlockedAttemptLogRepository logRepo)
        {
            _logRepo = logRepo;
        }

        public async Task<ServiceResult> AddAsync(BlockedAttemptLog log)
        {
            await _logRepo.AddAsync(log);
            return ServiceResult.Ok("Log recorded successfully");
        }

        public async Task<PagedResult<BlockAttemptDTO>> GetPagedAsync(int page, int pageSize)
        {
            var logs = await _logRepo.GetPagedAsync(page, pageSize);
            var mapped = logs.Select(l => new BlockAttemptDTO
            {
                IpAddress = l.IpAddress,
                CountryCode = l.CountryCode,
                IsBlocked = l.IsBlocked,
                UserAgent = l.UserAgent,
                Timestamp = l.Timestamp
            });

            return new PagedResult<BlockAttemptDTO>
            {
                Items = mapped.ToList(),
                Page = page,
                PageSize = pageSize,
                TotalCount = mapped.Count()
            };
        }

    }
}