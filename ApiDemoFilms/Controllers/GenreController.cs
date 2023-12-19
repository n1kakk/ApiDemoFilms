using ApiDemoFilms.Model;
using Microsoft.AspNetCore.Mvc;
using Films.DAL.InterfacesServices;

namespace ApiDemoFilms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController: Controller
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }


        [HttpGet("GetIdGenres/{id}")]
        [ProducesResponseType(200, Type = typeof(Genre))]
        public async Task<IActionResult> GetIdGenresAsync(int id)
        {
            var genre = await _genreService.GetIdGenresAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(genre);
        }


        [HttpGet("GetNameGenres/{genreName}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        public async Task<IActionResult> GetNameGenresAsync(string genreName)
        {
            var genres = await _genreService.GetNameGenresAsync(genreName);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(genres);
        }

        [HttpGet("GetGenres")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        public async Task<IActionResult> GetGenresAsync()
        {
            var genres = await _genreService.GetGenresAsync();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(genres);
        }
    }
}
