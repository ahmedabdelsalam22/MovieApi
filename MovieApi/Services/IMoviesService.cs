namespace MovieApi.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAll();
        Task<Movie> GetById(int id);

        Task<Movie> AddMovie(Movie movie);

        Movie Update(Movie movie);
        Movie Delete(int id);
    }
}
