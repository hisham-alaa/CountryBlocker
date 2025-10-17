using CountryBlocker.Application.DTOs;

namespace CountryBlocker.Application.Interfaces.IService
{
    public interface IIPCheckService
    {
        Task<ServiceResult<GeoInfo>> LookupIpAsync(string? ipAddress);

        Task<ServiceResult<BlockedCheckResultDTO>> CheckIfBlockedAsync(string ipAddress, string userAgent);
    }
}