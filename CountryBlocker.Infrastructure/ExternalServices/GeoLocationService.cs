using CountryBlocker.Application.DTOs;
using CountryBlocker.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace CountryBlocker.Infrastructure.ExternalServices
{
    public class GeoLocationService : IGeoLocationProvider
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeoLocationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GeoApi:ApiKey"] ?? string.Empty;
        }
        public async Task<GeoInfo?> GetGeoInfoAsync(string ip)
        {
            try
            {
                var url = $"https://ipapi.co/{ip}/json/?key={_apiKey}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var json = await response.Content.ReadFromJsonAsync<IpApiResponse>();
                if (json == null || string.IsNullOrEmpty(json.country_code))
                    return null;

                return new GeoInfo(
                    json.ip ?? ip,
                    json.country_code,
                    json.country_name ?? "Unknown",
                    json.org ?? "Unknown"
                );
            }
            catch (Exception ex)
            {
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
