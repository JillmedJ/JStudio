using JStudio.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace JStudio.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
            options) : base(options)
        {
        }

        public DbSet<Ad> Ads { get; set; } = null!;
    }
}
