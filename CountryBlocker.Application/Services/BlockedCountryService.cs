using CountryBlocker.Application.DTOs;
using CountryBlocker.Application.Interfaces.IRepository;
using CountryBlocker.Application.Interfaces.IService;
using CountryBlocker.Domain.Models;

namespace CountryBlocker.Application.Services
{
    public class BlockedCountryService : IBlockedCountryService
    {
        private readonly IBlockedCountryRepository _blockedCountryRepository;

        public BlockedCountryService(IBlockedCountryRepository blockedCountryRepository)
        {
            _blockedCountryRepository = blockedCountryRepository;
        }

        public async Task<ServiceResult> BlockCountryAsync(string countryCode, string countryName)
        {
            var existing = await _blockedCountryRepository.GetByCodeAsync(countryCode);
            if (existing != null)
                return ServiceResult.Fail($"Country '{countryCode}' is already blocked.");

            var country = new BlockedCountry { Code = countryCode, Name = countryName };
            await _blockedCountryRepository.AddAsync(country);
            return ServiceResult.Ok($"Country '{countryCode}' blocked successfully.");
        }

        public async Task<ServiceResult> UnblockCountryAsync(string countryCode)
        {
            var removed = await _blockedCountryRepository.RemoveAsync(countryCode);
            return removed
                ? ServiceResult.Ok($"Country '{countryCode}' unblocked successfully.")
                : ServiceResult.Fail($"Country '{countryCode}' is not currently blocked.");
        }

        public async Task<PagedResult<BlockedCountryDTO>> GetBlockedCountriesAsync(int page, int pageSize, string? search = null)
        {
            var countries = await _blockedCountryRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
                countries = countries.Where(c =>
                    c.Code.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    c.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

            var totalCount = countries.Count();
            var paged = countries
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new BlockedCountryDTO
                {
                    Code = c.Code,
                    Name = c.Name,
                    IsTemporarilyBlocked = c.IsTemporary,
                    ExpirationTimeUtc = c.ExpiryDate
                });

            return new PagedResult<BlockedCountryDTO>
            {
                Items = paged.ToList(),
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<ServiceResult> TemporarilyBlockCountryAsync(string countryCode, string countryName, int durationMinutes)
        {
            if (durationMinutes < 1 || durationMinutes > 1440)
                return ServiceResult.Fail("Duration must be between 1 and 1440 minutes.");

            var existing = await _blockedCountryRepository.GetByCodeAsync(countryCode);
            if (existing != null && existing.IsTemporary)
                return ServiceResult.Fail($"Country '{countryCode}' is already temporarily blocked.");

            var country = existing ?? new BlockedCountry { Code = countryCode, Name = countryName };
            country.ExpiryDate = DateTime.UtcNow.AddMinutes(durationMinutes);

            await _blockedCountryRepository.AddAsync(country);
            return ServiceResult.Ok($"Country '{countryCode}' temporarily blocked for {durationMinutes} minutes.");
        }

        public async void RemoveExpiredTemporalBlocks()
        {
            var countries = await _blockedCountryRepository.GetAllAsync();
            foreach (var country in countries)
            {
                if (country.IsExpired)
                {
                    country.ExpiryDate = null;
                    if (!country.IsTemporary)
                        await _blockedCountryRepository.RemoveAsync(country.Code);
                }
            }
        }
    }
}
