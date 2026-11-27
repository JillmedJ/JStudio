using JStudio.Api.DTOs;
using JStudio.Api.Entities;
using JStudio.Api.Services;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAd(int id)
        { 
            bool isDeleted = await _adService.DeleteAdAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // lyckat: ändringen sparad, inget att visa
        [ProducesResponseType(StatusCodes.Status404NotFound)] // annonsen finns inte
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // fel i ändringslistan eller bruten valideringsregel
        public async Task<IActionResult> PatchAd(int id, JsonPatchDocument<UpdateAdDto> patchDocument)
        {
            var updateAdDto = await _adService.GetAdToUpdateByIdAsync(id); // 1. Hämta annonsens nuvarande värden som en UpdateAdDto (null om id:t inte finns)

            if (updateAdDto == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(updateAdDto, error => ModelState.AddModelError("patch", error.ErrorMessage)); // 2. Lägg klientens ändringar (op/path/value) ovanpå de nuvarande värdena. // Fel i listan (t.ex. en path som inte finns, som "/id") sparas i ModelState.

            if (!TryValidateModel(updateAdDto)) // 3. Kör valideringsreglerna i UpdateAdDto ([Required], [Range], [Url] …) på resultatet.
                                               // Ger false om ApplyTo hittade fel ELLER om en regel bryts.
            {
                return ValidationProblem(); // 400 med felen, samma form som vanlig validering
            }

            await _adService.UpdateAdAsync(id, updateAdDto); // 4. Spara – återanvänder PUT:s servicemetod (DRY). Den sätter också Updated.

            return NoContent(); // 204 – lyckat, inget att skicka tillbaka (// låter negativt men är en kod av kategorin 200 nämligen 204)
        }
    } 
}