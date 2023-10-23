using ApiDemoFilms.Model;
using Films.DAL.Interfaces;
using Films.DAL.Repository;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        public async Task<IActionResult> GetIdGenresAsync(int id)
        {
            var films = await _genreRepository.GetIdGenresAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(films);
        }


        [HttpGet("GetNameGenres/{genreName}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        public async Task<IActionResult> GetNameGenresAsync(string genreName)
        {
            var films = await _genreRepository.GetNameGenresAsync(genreName);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(films);
        }

        [HttpGet("GetGenres")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        public async Task<IActionResult> GetGenresAsync()
        {
            var films = await _genreRepository.GetGenresAsync();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(films);
        }
    }
}
