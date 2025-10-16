using CountryBlocker.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CountryBlocker.Api.Controllers
{
    [Route("api/logs")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly BlockedAttemptLogService _logService;

        public LogsController(BlockedAttemptLogService logService)
        {
            _logService = logService;
        }

        [HttpGet("blocked-attempts")]
        public IActionResult GetBlockedAttempts()
        {
            var logs = _logService.GetLogs();
            return Ok(logs);
        }
    }
}