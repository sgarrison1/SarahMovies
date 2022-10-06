using System.ComponentModel.DataAnnotations;

namespace SarahMovies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Url]
        public string? IMDB { get; set; }
        public string? Genre { get; set; }
        public int ReleaseYear { get; set; }

        public byte[]? MediaMovie { get;set; }
    }
}
