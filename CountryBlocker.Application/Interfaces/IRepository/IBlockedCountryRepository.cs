using CountryBlocker.Domain.Models;

namespace CountryBlocker.Application.Interfaces.IRepository
{
    public interface IBlockedCountryRepository
    {
        Task AddAsync(BlockedCountry country);
        Task<bool> RemoveAsync(string countryCode);
        Task<BlockedCountry?> GetByCodeAsync(string countryCode);
        Task<IEnumerable<BlockedCountry>> GetAllAsync();
    }
}