using CountryBlocker.Application.Interfaces.IRepository;
using CountryBlocker.Domain.Models;
using System.Collections.Concurrent;

namespace CountryBlocker.Infrastructure.Repositories
{
    public class InMemoryBlockedCountryRepository : IBlockedCountryRepository
    {
        private readonly ConcurrentDictionary<string, BlockedCountry> _countries = new(StringComparer.OrdinalIgnoreCase);

        public Task AddAsync(BlockedCountry country)
        {
            _countries[country.Code] = country;
            return Task.CompletedTask;
        }

        public Task<bool> RemoveAsync(string countryCode)
        {
            var removed = _countries.TryRemove(countryCode, out _);
            return Task.FromResult(removed);
        }

        public Task<BlockedCountry?> GetByCodeAsync(string countryCode)
        {
            _countries.TryGetValue(countryCode, out var country);
            return Task.FromResult(country);
        }

        public Task<IEnumerable<BlockedCountry>> GetAllAsync()
        {
            var values = _countries.Values.AsEnumerable();
            return Task.FromResult(values);
        }

    }
}