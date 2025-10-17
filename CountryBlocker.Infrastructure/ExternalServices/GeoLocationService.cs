using CountryBlocker.Application.DTOs;
using CountryBlocker.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CountryBlocker.Infrastructure.ExternalServices
{
    public class GeoLocationService : IGeoLocationProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<GeoLocationService> _logger;
        private readonly string _apiKey;

        public GeoLocationService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<GeoLocationService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _apiKey = configuration["GeoApi:ApiKey"] ?? string.Empty;
        }

        public async Task<GeoInfo?> GetGeoInfoAsync(string ip)
        {
            var client = _httpClientFactory.CreateClient("IpApi-Client");

            try
            {
                var url = $"{ip}/json";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to fetch IP info for {Ip}. Status: {Status}", ip, response.StatusCode);
                    return null;
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<IpApiResponse>(jsonString);

                if (data == null || string.IsNullOrEmpty(data.country_code))
                    return null;

                return new GeoInfo
                {
                    Ip = data.ip ?? ip,
                    CountryCode = data.country_code,
                    CountryName = data.country_name ?? "Unknown",
                    ISP = data.org ?? "Unknown"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching geolocation for IP {Ip}", ip);
                return null;
            }
        }

        private class IpApiResponse
        {
            public string? ip { get; set; }
            public string? country_code { get; set; }
            public string? country_name { get; set; }
            public string? org { get; set; }
        }
    }
}