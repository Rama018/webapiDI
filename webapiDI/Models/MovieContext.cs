using Microsoft.EntityFrameworkCore;

namespace webapiDI.Models
{
    public class MovieContext :DbContext
    {
        public MovieContext(DbContextOptions<MovieContext>options)
            : base(options)
        {

        }
        public DbSet<Movie> Move { get; set; }

    }
}
