using CountryBlocker.Domain.Models;

namespace CountryBlocker.Domain.Interfaces
{
    public interface IBlockedCountryRepository
    {
        void Add(BlockedCountry country);

        bool Exists(string countryCode);

        bool Remove(string countryCode);

        IEnumerable<BlockedCountry> GetAll();
    }
}