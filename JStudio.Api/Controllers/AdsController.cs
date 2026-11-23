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

    } 
}