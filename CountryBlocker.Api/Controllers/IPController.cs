using CountryBlocker.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace CountryBlocker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IPController : ControllerBase
    {
        private readonly IIPCheckService _ipService;

        public IPController(IIPCheckService ipService)
        {
            _ipService = ipService;
        }

        [HttpGet("lookup")]
        public async Task<IActionResult> Lookup([FromQuery] string ip)
        {
            var result = await _ipService.LookupIpAsync(ip);
            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpGet("check")]
        public async Task<IActionResult> Check([FromQuery] string ip)
        {
            var userAgent = Request.Headers.UserAgent.ToString();
            var result = await _ipService.CheckIfBlockedAsync(ip, userAgent);
            return Ok(result.Data);
        }
    }
}