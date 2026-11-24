using JStudio.Api.Data;
using JStudio.Api.DTOs;
using JStudio.Api.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace JStudio.Api.Services
{
    public class AdService : IAdService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AdService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Ad>> GetAllAdsAsync()
        {
            // Ads = tabellen (DbSet<Ad>). ToListAsync hämtar alla rader som en lista.
            // Motsvarar SQL: SELECT * FROM Ads
            return await _applicationDbContext.Ads.ToListAsync();
        }

        public async Task<Ad?> GetSingleAdByIdAsync(int id)
        {
            return await _applicationDbContext.Ads.FindAsync(id);
        }

        public async Task<Ad> CreateAdAsync(CreateAdDto createAdDto)
        {
            var ad = new Ad
            {
                Title = createAdDto.Title,
                Price = createAdDto.Price,
                Description = createAdDto.Description,
                Location = createAdDto.Location,
                ImageUrl = createAdDto.ImageUrl,
                Category = createAdDto.Category,
                Created = DateTime.UtcNow
            };

            _applicationDbContext.Ads.Add(ad);

            await _applicationDbContext.SaveChangesAsync();

            return ad;

        }

        public async Task<bool> UpdateAdAsync(int id, UpdateAdDto updateAdDto)
        {
            var adToUpdate = await GetSingleAdByIdAsync(id);

            if (adToUpdate == null)
            {
                return false;
            }

            adToUpdate.Title = updateAdDto.Title;
            adToUpdate.Price = updateAdDto.Price;
            adToUpdate.Description = updateAdDto.Description;
            adToUpdate.Location = updateAdDto.Location;
            adToUpdate.ImageUrl = updateAdDto.ImageUrl;
            adToUpdate.Category = updateAdDto.Category;
            adToUpdate.Updated = DateTime.UtcNow;

            await _applicationDbContext.SaveChangesAsync();

            return true;

        }
    }
}
