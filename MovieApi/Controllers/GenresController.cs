using MovieApi.Services;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres =await _genresService.GetAll();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateGenreDto dto)
        {
            var genre = new Genre()
            {
                Name = dto.Name
            };

            await _genresService.CreateGenre(genre);
            return Ok(genre);
        }

        [HttpPut(template: "{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] CreateGenreDto dto)
        {
            var genre = await _genresService.GetById(id);

            if (genre == null)
            {
                return NotFound(value: $"no genre is found with Id: {id}");
            }

            genre.Name = dto.Name;
            _genresService.UpdateGenre(genre);
            return Ok(genre);
        }

        [HttpDelete(template: "{id}")]

        public async Task<IActionResult> DeleteAsync(byte id) 
        {
          var genre = await _genresService.GetById(id);
            if (genre == null)
            {
                return NotFound(value: $"no genre is found with Id: {id}");
            }

            _genresService.DeleteGenre(genre);
            return Ok(genre);
        }

    }
}
