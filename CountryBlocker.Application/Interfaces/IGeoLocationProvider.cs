using CountryBlocker.Application.DTOs;

namespace CountryBlocker.Application.Interfaces
{
    public interface IGeoLocationProvider
    {
        Task<GeoInfo?> GetGeoInfoAsync(string ip);
    }
}