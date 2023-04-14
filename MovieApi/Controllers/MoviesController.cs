using AutoMapper;
using AutoMapper.Configuration.Annotations;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Services;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;

        private readonly IMoviesService _moviesService;
        private readonly IGenresService _genresService;
        private readonly IMapper _mapper;

        public MoviesController(IMoviesService moviesService, IGenresService genresService, IMapper mapper)
        {
            _moviesService = moviesService;
            _genresService = genresService;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllAsync()  ///// to return genre name 
        {
            var movies = await _moviesService.GetAll();
            
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

            return Ok(data);
        }


        [HttpGet(template: "{id}")]
        public async Task<IActionResult> GetByIdAsync(int id) 
        {
            var movie = await _moviesService.GetById(id); 


            if (movie == null)     
               return NotFound(value: $"no Movie is found with Id: {id}");

            var dto = _mapper.Map<MovieDetailsDto>(movie);

            return Ok(dto);
            
        }

        [HttpGet(template: "GetByGenreId")]
        public async Task<IActionResult> GetMovieByGenreId(byte genreId)
        {
            var movie = await _moviesService.GetAll(genreId);

            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movie);

            if (data == null)
                return NotFound(value: $"no Movie is found with this GenreId: {genreId}");

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieDto dto) 
        {

            if (dto.Poster == null)
                return BadRequest("Poster is required!");

            if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed!");

            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 1MB!");

            var isValidGenre =await _genresService.IsValidGenre(dto.GenreId);
            if (!isValidGenre)
                return BadRequest("Invalid genere ID!");

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var movie = _mapper.Map<Movie>(dto);
            movie.Poster = dataStream.ToArray();

            await _moviesService.AddMovie(movie);
            return Ok(movie);
        }


        [HttpPut(template:"{id}")]
        public async Task<IActionResult> UpdateAsync(int id,[FromForm]MovieDto dto) 
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null) NotFound(value: $"no Movie is found with id: {id}");
            var isValidGenre = await _genresService.IsValidGenre(dto.GenreId);
            if (!isValidGenre)  return BadRequest("Invalid genere ID!");

            if (dto.Poster != null) 
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg images are allowed!");

                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max allowed size for poster is 1MB!");

                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }

           

            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.Year = dto.Year;
            movie.Rate = dto.Rate;

            _moviesService.Update(movie);
            return Ok(movie);

        }


        [HttpDelete(template: "{id}")]
        public async Task<IActionResult> DeleteAsync(int id) 
        {
            var movie =await _moviesService.GetById(id);

            if(movie == null) return NotFound(value: $"no Movie is found with Id: {id} to delete");
            
             _moviesService.Delete(movie);

            return Ok();
        }


    }
}