namespace CountryBlocker.Application.DTOs
{
    public class BlockedCountryDTO
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsTemporarilyBlocked { get; set; }
        public DateTime? ExpirationTimeUtc { get; set; }
    }
}