using System.Threading.Tasks;
using CountryBlocker.Application.DTOs;

namespace CountryBlocker.Application.Interfaces
{
    public interface IGeoLocationProvider
    {
        public Task<GeoInfo?> GetGeoInfoAsync(string ip);
    }
}