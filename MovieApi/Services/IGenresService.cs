namespace MovieApi.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> CreateGenre(Genre genre);
    }
}
