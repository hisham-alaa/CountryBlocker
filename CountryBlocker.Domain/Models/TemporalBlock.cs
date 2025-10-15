namespace CountryBlocker.Domain.Models
{
    public class TemporalBlock
    {
        public string CountryCode { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }

        public bool IsExpired => DateTime.UtcNow > ExpiryDate;
    }
}