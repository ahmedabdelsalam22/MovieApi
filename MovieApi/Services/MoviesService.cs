namespace MovieApi.Services
{
    public class MoviesService : IMoviesService
    {
        public Task<IEnumerable<Movie>> GetAll()
        {
            throw new NotImplementedException();
        }
        public Task<Movie> AddMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
        public Task<Movie> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Movie Update(Movie movie)
        {
            throw new NotImplementedException();
        }
        public Movie Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
