using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SarahMovies.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        [Url]
        public string? ActorIMBD { get; set; }
        
        public byte[]? MediaActor { get; set; }

        [ForeignKey("Movie")] 
        public int? MovieID { get; set; }
        public Movie? Movie { get; set; }


    }
}
