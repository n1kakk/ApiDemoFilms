using ApiDemoFilms.Model;
using Films.DAL.InterfacesServices;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemoFilms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController: Controller
    {
        private readonly IFilmService _filmService; //поле только для чтения
        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;  //ридонли установлено в конструкторе
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Film>))]
        public async Task<IActionResult> GetFilms()
        {
            var films = await _filmService.GetFilms();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(films);
        }


        [HttpGet("GetGenreFilms/{genre}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Film>))]
        public async Task<IActionResult> GetGenreFilmsAsync(string genre)
        {
            var films = await _filmService.GetGenreFilmsAsync(genre);
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            return Ok(films);
        }

        [HttpGet("GetReleaseYearFilms/{releaseYear}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Film>))]
        public async Task<IActionResult> GetReleaseYearFilmsAsync(int releaseYear)
        {
            var films = await _filmService.GetReleaseYearFilmsAsync(releaseYear);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(films);
        }

        [HttpGet("GetDirectorFilms/{directorId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Film>))]
        public async Task<IActionResult> GetDirectorFilmsAsync(int directorId)
        {
            var films = await _filmService.GetDirectorFilmsAsync(directorId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(films);
        }

        [HttpGet("GetIdFilms/{id}")]
        [ProducesResponseType(200, Type = typeof(Film))]
        public async Task<IActionResult> GetIdFilmsAsync(int id)
        {
            var film = await _filmService.GetIdFilmsAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(film);
        }

    }
}
