using CountryBlocker.Application.DTOs;

namespace CountryBlocker.Application.Interfaces
{
    public interface IGeoLocationService
    {
        Task<GeoInfoDTO?> GetGeoInfoAsync(string ip);
    }
}