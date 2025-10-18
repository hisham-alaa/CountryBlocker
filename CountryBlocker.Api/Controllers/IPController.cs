using CountryBlocker.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace CountryBlocker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IPController : ControllerBase
    {
        private readonly IIPCheckService _ipService;
        private readonly IHttpContextAccessor _contextAccessor;

        public IPController(IIPCheckService ipService, IHttpContextAccessor contextAccessor)
        {
            _ipService = ipService;
            _contextAccessor = contextAccessor;
        }

        [HttpGet("lookup")]
        public async Task<IActionResult> Lookup([FromQuery] string? ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
                ip = GetClientIp();

            var result = await _ipService.LookupIpAsync(ip);
            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpGet("check")]
        public async Task<IActionResult> Check([FromQuery] string? ip)
        {
            var userAgent = Request.Headers.UserAgent.ToString();
            ip = GetClientIp();
            var result = await _ipService.CheckIfBlockedAsync(ip, userAgent);
            return Ok(result.Data);
        }

        private string? GetClientIp()
        {
            var context = _contextAccessor.HttpContext;
            if (context == null)
                return null;

            // Check for proxy headers first
            var forwardedHeader = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedHeader))
                return forwardedHeader.Split(',').FirstOrDefault()?.Trim();

            // Fallback to direct connection IP
            return context.Connection.RemoteIpAddress?.ToString();
        }
    }
}