using ApiDemoFilms.Model;
using Films.DAL.Interfaces;
using Films.DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Films.DAL.Helpers;

namespace ApiDemoFilms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController: Controller
    {
        private readonly IGenreRepository _genreRepository;
        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }


        [HttpGet("GetIdGenres/{id}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(Genre))]
        public async Task<IActionResult> GetIdGenresAsync(int id)
        {
            var genre = await _genreRepository.GetIdGenresAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(genre);
        }


        [HttpGet("GetNameGenres/{genreName}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        public async Task<IActionResult> GetNameGenresAsync(string genreName)
        {
            var genres = await _genreRepository.GetNameGenresAsync(genreName);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(genres);
        }

        [HttpGet("GetGenres")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        public async Task<IActionResult> GetGenresAsync()
        {
            var genres = await _genreRepository.GetGenresAsync();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(genres);
        }
    }
}
