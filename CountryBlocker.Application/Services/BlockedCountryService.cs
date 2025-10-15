using CountryBlocker.Domain.Interfaces;
using CountryBlocker.Domain.Models;

namespace CountryBlocker.Application.Services
{
    public class BlockedCountryService
    {
        private readonly IBlockedCountryRepository _blockedCountryRepository;

        public BlockedCountryService(IBlockedCountryRepository blockedCountryRepository)
        {
            _blockedCountryRepository = blockedCountryRepository;
        }

        public void BlockCountry(string countryCode)
        {
            if (_blockedCountryRepository.Exists(countryCode))
            {
                throw new InvalidOperationException($"Country with code {countryCode} is already blocked.");
            }

            _blockedCountryRepository.Add(new BlockedCountry
            {
                CountryCode = countryCode
            });
        }

        public bool UnBlockCountry(string countryCode)
        {
            if (!_blockedCountryRepository.Exists(countryCode))
            {
                throw new KeyNotFoundException($"Country with code {countryCode} is not blocked.");
            }
            return _blockedCountryRepository.Remove(countryCode);
        }

        public IEnumerable<BlockedCountry> GetAllBlockedCountries()
        {
            return _blockedCountryRepository.GetAll();
        }
    }
}