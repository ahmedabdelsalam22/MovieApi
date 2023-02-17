using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly ApplicationDbContext _context;

        public MoviesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0)
        {
            var movies = await _context.Movies
                .Where(x => x.GenreId == genreId || genreId == 0)
                .Include(x => x.Genre)
                
               .ToListAsync();

            return movies;
        }
        public async Task<Movie> GetById(int id)
        {
            var movies = await _context.Movies.Include(g => g.Genre).SingleOrDefaultAsync(m => m.Id == id);
            return movies;
        }
        public async Task<Movie> AddMovie(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            _context.SaveChanges();
            return movie;
        }       
        public  Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return movie;
        }
        public Movie Delete(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();

            return movie;
        } 
    }
}
