using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SarahMovies.Models;

namespace SarahMovies.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SarahMovies.Models.Actor> Actor { get; set; }
        public DbSet<SarahMovies.Models.Movie> Movie { get; set; }
        public DbSet<SarahMovies.Models.MovieActor> MovieActor { get; set; }
    }
}