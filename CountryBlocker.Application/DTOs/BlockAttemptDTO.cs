﻿namespace CountryBlocker.Application.DTOs
{
    public class BlockAttemptDTO
    {
        public string IpAddress { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public bool IsBlocked { get; set; }
        public string UserAgent { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}