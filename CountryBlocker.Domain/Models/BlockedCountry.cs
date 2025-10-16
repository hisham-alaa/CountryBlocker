namespace CountryBlocker.Domain.Models
{
    public class BlockedCountry
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        // Used for temporary blocks
        public DateTime? ExpiryDate { get; set; }

        public bool IsTemporary => ExpiryDate.HasValue;

        public bool IsExpired => IsTemporary && ExpiryDate <= DateTime.UtcNow;

    }
}