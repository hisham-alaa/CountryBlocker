namespace CountryBlocker.Application.DTOs
{
    public class TemporalBlockCountry
    {
        public string CountryCode { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
    }
}