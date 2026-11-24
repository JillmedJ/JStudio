using JStudio.Api.Entities;

namespace JStudio.Api.DTOs
{
    public class CreateAdDto
    {
        // By ad owner
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public AdCategory Category { get; set; }
    }
}