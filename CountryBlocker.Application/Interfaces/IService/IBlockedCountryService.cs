using CountryBlocker.Application.DTOs;

namespace CountryBlocker.Application.Interfaces.IService
{
    public interface IBlockedCountryService
    {
        Task<ServiceResult> BlockCountryAsync(string countryCode, string countryName);

        Task<ServiceResult> UnblockCountryAsync(string countryCode);

        Task<PagedResult<BlockedCountryDTO>> GetBlockedCountriesAsync(int page, int pageSize, string? code = null, string? name = null);

        Task<ServiceResult> TemporarilyBlockCountryAsync(string countryCode, string countryName, int durationMinutes);

        void RemoveExpiredTemporalBlocks();
    }
}