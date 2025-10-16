using CountryBlocker.Application.DTOs;
using CountryBlocker.Domain.Models;

namespace CountryBlocker.Application.Interfaces.IService
{
    public interface IBlockedAttemptLogService
    {
        Task<ServiceResult> AddAsync(BlockedAttemptLog log);
        Task<PagedResult<BlockAttemptDTO>> GetPagedAsync(int page, int pageSize);
    }
}
