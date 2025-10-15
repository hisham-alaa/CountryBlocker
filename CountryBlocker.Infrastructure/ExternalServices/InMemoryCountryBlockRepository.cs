using CountryBlocker.Domain.Interfaces;
using CountryBlocker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryBlocker.Infrastructure.ExternalServices
{
    public class InMemoryCountryBlockRepository : IBlockedCountryRepository
    {
        public void Add(BlockedCountry country)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string countryCode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlockedCountry> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Remove(string countryCode)
        {
            throw new NotImplementedException();
        }
    }
}