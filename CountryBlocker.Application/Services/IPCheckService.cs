using CountryBlocker.Application.DTOs;
using CountryBlocker.Application.Interfaces;
using CountryBlocker.Application.Interfaces.IRepository;
using CountryBlocker.Application.Interfaces.IService;
using CountryBlocker.Domain.Models;

namespace CountryBlocker.Application.Services
{
    public class IPCheckService : IIPCheckService
    {
        private readonly IGeoLocationProvider _geoProvider;
        private readonly IBlockedCountryRepository _countryRepository;
        private readonly IBlockedAttemptLogRepository _logService;
        public IPCheckService(
        IGeoLocationProvider geoProvider,
        IBlockedCountryRepository countryRepository,
        IBlockedAttemptLogRepository logService)
        {
            _geoProvider = geoProvider;
            _countryRepository = countryRepository;
            _logService = logService;
        }

        public async Task<ServiceResult<GeoInfo>> LookupIpAsync(string? ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                return ServiceResult<GeoInfo>.Fail("IP address is required.");

            var geoInfo = await _geoProvider.GetGeoInfoAsync(ipAddress);
            if (geoInfo == null)
                return ServiceResult<GeoInfo>.Fail("Failed to retrieve geolocation info.");

            return ServiceResult<GeoInfo>.Ok(geoInfo);
        }

        public async Task<ServiceResult<BlockedCheckResultDTO>> CheckIfBlockedAsync(string ipAddress, string userAgent)
        {
            var geoInfo = await _geoProvider.GetGeoInfoAsync(ipAddress);
            if (geoInfo == null)
                return ServiceResult<BlockedCheckResultDTO>.Fail("Unable to determine IP location.");

            var country = await _countryRepository.GetByCodeAsync(geoInfo.CountryCode);
            var isBlocked = country != null && (!country.IsTemporary || !country.IsExpired);

            var log = new BlockedAttemptLog(ipAddress, geoInfo.CountryCode, isBlocked, userAgent);

            await _logService.AddAsync(log);

            var result = new BlockedCheckResultDTO
            {
                IpAddress = ipAddress,
                CountryCode = geoInfo.CountryCode,
                IsBlocked = isBlocked,
                Message = isBlocked
                    ? $"Access blocked. Country '{geoInfo.CountryCode}' is restricted."
                    : "Access allowed."
            };

            return ServiceResult<BlockedCheckResultDTO>.Ok(result);
        }



    }

}



