using CountryBlocker.Application.DTOs;
using CountryBlocker.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly IBlockedCountryService _blockedCountryService;

        public CountriesController(IBlockedCountryService countryService)
        {
            _blockedCountryService = countryService;
        }

        [HttpPost("block")]
        public async Task<IActionResult> BlockCountry([FromBody] BlockedCountryRequest request)
        {
            var result = await _blockedCountryService.BlockCountryAsync(request.CountryCode, request.CountryName);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("block-temporary")]
        public async Task<IActionResult> BlockTemporary([FromBody] TemporaryBlockRequest request)
        {
            var result = await _blockedCountryService.TemporarilyBlockCountryAsync(request.CountryCode, request.CountryName, request.DurationMinutes);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("block/{countryCode}")]
        public async Task<IActionResult> UnblockCountry(string countryCode)
        {
            var result = await _blockedCountryService.UnblockCountryAsync(countryCode);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost("blocked")]
        public async Task<IActionResult> GetBlockedCountries([FromBody] GetBlockedCountriesRequest request)
        {
            var countries = await _blockedCountryService.GetBlockedCountriesAsync(request.Page, request.PageSize, request.CountryCode, request.CountryName);
            return Ok(countries);
        }
    }

    public class BlockedCountryRequest
    {
        public string CountryCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
    }

    public class TemporaryBlockRequest : BlockedCountryRequest
    {
        public int DurationMinutes { get; set; }
    }
}