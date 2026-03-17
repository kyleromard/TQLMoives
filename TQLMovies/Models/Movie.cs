namespace TQLMovies.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public int RuntimeMinutes { get; set; }
    }
}

