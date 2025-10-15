namespace CountryBlocker.Application.DTOs
{
    public class GeoInfoDTO
    {
        public string Ip { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string ISP { get; set; } = string.Empty;
    }
}