using CountryBlocker.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace CountryBlocker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly IBlockedAttemptLogService _logService;

        public LogsController(IBlockedAttemptLogService logService)
        {
            _logService = logService;
        }

        [HttpGet("blocked-attempts")]
        public async Task<IActionResult> GetBlockedAttempts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var logs = await _logService.GetPagedAsync(page, pageSize);
            return Ok(logs);
        }
    }
}