using CountryBlocker.Domain.Models;

namespace CountryBlocker.Domain.Interfaces
{
    public interface ITemporalBlockedCountryRepository
    {
        void Add(TemporalBlock block);

        bool Exists(string countryCode);

        void RemoveExpired();

        IEnumerable<TemporalBlock> GetAll();
    }
}