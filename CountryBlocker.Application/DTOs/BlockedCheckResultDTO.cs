namespace CountryBlocker.Application.DTOs
{
    public class BlockedCheckResultDTO
    {
        public string IpAddress { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public bool IsBlocked { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}