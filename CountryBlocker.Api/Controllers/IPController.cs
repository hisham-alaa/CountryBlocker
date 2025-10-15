using CountryBlocker.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CountryBlocker.Api.Controllers
{
    [Route("api/ip")]
    [ApiController]
    public class IpController : ControllerBase
    {
        private readonly IPCheckService _ipCheckService;

        public IpController(IPCheckService ipCheckService)
        {
            _ipCheckService = ipCheckService;
        }

        [HttpGet("check-block")]
        public async Task<IActionResult> CheckBlock()
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

            if (string.IsNullOrEmpty(ip))
                return BadRequest(new { Message = "Unable to determine IP address" });

            var userAgent = Request.Headers["User-Agent"].ToString();
            var isBlocked = await _ipCheckService.IsIpBlockedAsync(ip, userAgent);

            return Ok(new
            {
                Ip = ip,
                IsBlocked = isBlocked
            });
        }
    }
}