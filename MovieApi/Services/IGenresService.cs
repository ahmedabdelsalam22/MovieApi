namespace MovieApi.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAll();
    }
}
