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
        public async Task<ActionResult<List<Ad>>> GetAllAds()
        {
            var ads = await _adService.GetAllAdsAsync();

            return Ok(ads);
        }

        [HttpGet("{id}")]
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
        public async Task<ActionResult<Ad>> CreateAd(CreateAdDto createAdDto)
        { 
            var ad = await _adService.CreateAdAsync(createAdDto);

            return CreatedAtAction(nameof(GetSingleAdById), new { id = ad.Id }, ad );
        }

    } 
}