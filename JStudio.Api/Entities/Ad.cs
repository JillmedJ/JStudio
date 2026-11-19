// var typen ska leta för att få det den behöver (bor den andra typen på samma adress behovs ingen using)

namespace JStudio.Api.Entities //var typen bor
{
    public class Ad
    {
        // By system

        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; } // ? informerar att okej att vara null

        // By ad owner

        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty; // passar vid Code First (= null!; passar/kommer från databasen vid möjligt null)

        public string Location { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public AdCategory Category { get; set; }
    }
}
