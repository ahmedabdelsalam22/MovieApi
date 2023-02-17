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
        public Task<Genre> CreateGenre(Genre genre)
        {
            throw new NotImplementedException();
        }

        public Task<Genre> UpdateGenre(Genre genre)
        {
            throw new NotImplementedException();
        }
        public Task<Genre> DeleteGenre(Genre genre)
        {
            throw new NotImplementedException();
        }     
    }
}
