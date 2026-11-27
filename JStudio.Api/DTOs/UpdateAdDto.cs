using JStudio.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace JStudio.Api.DTOs
{
    public class UpdateAdDto
    {
        // By ad owner
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Range(1, 3000)] // Pris 1–3000 kr:
                         // - Minst 1 kr, eftersom "Bortskänkes" inte finns som alternativ.
                         // - Max 3000 kr: sajten är ingen specialsajt för samlarobjekt, men dyra pennor
                         //   kan finnas på andrahandsmarknaden. Ett högt men inte orimligt tak.
        public decimal Price { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Location { get; set; } = string.Empty;

        [Url]
        public string? ImageUrl { get; set; }

        [Range(1, 8)] // 1 = FinelinerPens … 8 = Other. NotSet (0) nekas. Uppdatera max vid ny kategori! Kom ihåg att ifall denna uppdateras så behöver även CreateAdDto uppdateras.
        public AdCategory Category { get; set; }
    }
}