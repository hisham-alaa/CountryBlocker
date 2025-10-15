namespace CountryBlocker.Domain.Models
{
    public class BlockedCountry
    {
        public string CountryCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;

        // Used for temporary blocks
        public DateTime? ExpiryDate { get; set; }

        public bool IsTemporary => ExpiryDate.HasValue;

        public bool IsExpired => IsTemporary && ExpiryDate <= DateTime.UtcNow;
    }
}