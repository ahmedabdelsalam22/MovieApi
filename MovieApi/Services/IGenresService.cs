namespace MovieApi.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAll();

        Task<Genre> GetById(byte id);
        Task<Genre> CreateGenre(Genre genre);

        Task<Genre> UpdateGenre(Genre genre);

        Task<Genre> DeleteGenre(Genre genre);
    }
}
