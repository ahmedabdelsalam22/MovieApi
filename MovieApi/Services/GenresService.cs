using Microsoft.EntityFrameworkCore;

namespace MovieApi.Services
{
    public class GenresService : IGenresService
    {
        private readonly ApplicationDbContext _context;

        public GenresService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            var genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
            return genres;
        }
        public async Task<Genre> GetById(byte id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
            return genre;
        }
        public async Task<Genre> CreateGenre(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre UpdateGenre(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();

            return genre;
        }
        public Genre DeleteGenre(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();

            return genre;
        }

     
    }
}
