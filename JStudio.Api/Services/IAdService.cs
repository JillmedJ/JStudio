using JStudio.Api.DTOs;
using JStudio.Api.Entities;  // Ad bor i Entities

namespace JStudio.Api.Services; // Interfacet bor i Services

public interface IAdService
{
    // Hämtar ALLA annonser. Returnerar en lista med Ad.
    Task<List<Ad>> GetAllAdsAsync();

    // Hämtar EN annons. Tar emot ett id (typ: int, namn: id).
    // Ad? = annonsen kan saknas (null) om id:t inte finns.
    Task<Ad?> GetSingleAdByIdAsync(int id);

    Task<Ad> CreateAdAsync(CreateAdDto createAdDto);

    Task<bool> UpdateAdAsync(int id, UpdateAdDto updateAdDto);

    Task<bool> DeleteAdAsync(int id);

    Task<UpdateAdDto?> GetAdToUpdateByIdAsync(int id);
}