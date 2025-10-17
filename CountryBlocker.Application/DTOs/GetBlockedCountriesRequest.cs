namespace CountryBlocker.Application.DTOs
{
    public class GetBlockedCountriesRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
    }
}