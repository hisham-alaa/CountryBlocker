using CountryBlocker.Application.DTOs;
using CountryBlocker.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CountryBlocker.Api.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly BlockedCountryService _countryService;

        public CountryController(BlockedCountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpPost("block")]
        public IActionResult BlockCountry([FromBody] BlockCountryRequest request)
        {
            try
            {
                _countryService.BlockCountry(request.CountryCode);
                return Ok(new { Message = $"Country {request.CountryCode} blocked successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("unblock/{countryCode}")]
        public IActionResult UnblockCountry(string countryCode)
        {
            var success = _countryService.UnBlockCountry(countryCode);
            if (!success)
                return NotFound(new { Message = "Country not found or already unblocked" });

            return Ok(new { Message = $"Country {countryCode} unblocked successfully" });
        }

        [HttpGet("blocked")]
        public IActionResult GetBlockedCountries()
        {
            var blocked = _countryService.GetAllBlockedCountries();
            return Ok(blocked);
        }
    }
}