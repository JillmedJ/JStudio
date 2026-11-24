using JStudio.Api.DTOs;
using JStudio.Api.Entities;
using JStudio.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace JStudio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdsController : ControllerBase
    {
        private readonly IAdService _adService;

        public AdsController(IAdService adService)
        {
            _adService = adService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // Säger till Swagger hur den ska agera beroende på return
        public async Task<ActionResult<List<Ad>>> GetAllAds()
        {
            var ads = await _adService.GetAllAdsAsync();

            return Ok(ads);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Ad>> GetSingleAdById(int id)
        {
            var ad = await _adService.GetSingleAdByIdAsync(id);

            if (ad == null)
            {
                return NotFound();
            }

            return Ok(ad);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Ad>> CreateAd(CreateAdDto createAdDto)
        { 
            var ad = await _adService.CreateAdAsync(createAdDto);

            return CreatedAtAction(nameof(GetSingleAdById), new { id = ad.Id }, ad );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAd(int id, UpdateAdDto updateAdDto)
        {
            bool isUpdated = await _adService.UpdateAdAsync(id, updateAdDto);

            if (!isUpdated)
            {
                return NotFound();
            }
            
            return NoContent(); // låter negativt men är en kod av kategorin 200 nämligen 204
        }
    } 
}