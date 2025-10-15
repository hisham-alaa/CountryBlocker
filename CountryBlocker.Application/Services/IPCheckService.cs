using CountryBlocker.Application.Interfaces;
using CountryBlocker.Domain.Interfaces;
using CountryBlocker.Domain.Models;

namespace CountryBlocker.Application.Services
{
    public class IPCheckService
    {
        private readonly IGeoLocationService _geoService;
        private readonly IBlockedCountryRepository _blockRepo;
        private readonly ILogRepository _logRepo;

        public IPCheckService(
            IGeoLocationService geoService,
            IBlockedCountryRepository blockRepo,
            ILogRepository logRepo)
        {
            _geoService = geoService;
            _blockRepo = blockRepo;
            _logRepo = logRepo;
        }

        public async Task<bool> IsIpBlockedAsync(string ip, string userAgent)
        {
            var geoInfo = await _geoService.GetGeoInfoAsync(ip);

            if (geoInfo is null)
                throw new InvalidOperationException("Unable to fetch IP info.");

            var isBlocked = _blockRepo.Exists(geoInfo.CountryCode);

            _logRepo.Add(new BlockAttemptLog
            {
                IpAddress = ip,
                IsBlocked = isBlocked,
                UserAgent = userAgent,
                Timestamp = DateTime.UtcNow
            });

            return isBlocked;
        }
    }
}