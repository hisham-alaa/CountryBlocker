namespace CountryBlocker.Domain.Models
{
    public class BlockedAttemptLog
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string IpAddress { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public bool IsBlocked { get; set; }
        public string UserAgent { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public BlockedAttemptLog(string ipAddress, string countryCode, bool isBlocked, string userAgent)
        {
            IpAddress = ipAddress;
            CountryCode = countryCode.ToUpperInvariant();
            IsBlocked = isBlocked;
            UserAgent = userAgent;
        }
    }
}