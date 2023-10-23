using ApiDemoFilms.Model;
using Films.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemoFilms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController: Controller
    {
        private readonly IFilmRepository _filmRepository; //поле только для чтения
        public FilmController(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;  //ридонли установлено в конструкторе
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Film>))]
        public IActionResult GetFilms()
        {
            var films = _filmRepository.GetFilms();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(films);
        }


        [HttpGet("GetGenreFilms/{genre}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Film>))]
        public async Task<IActionResult> GetGenreFilmsAsync(string genre)
        {
            var films = await _filmRepository.GetGenreFilmsAsync(genre);
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            return Ok(films);
        }

        [HttpGet("GetReleaseYearFilms/{releaseYear}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Film>))]
        public async Task<IActionResult> GetReleaseYearFilmsAsync(int releaseYear)
        {
            var films = await _filmRepository.GetReleaseYearFilmsAsync(releaseYear);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(films);
        }

        [HttpGet("GetDirectorFilms/{directorId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Film>))]
        public async Task<IActionResult> GetDirectorFilmsAsync(int directorId)
        {
            var films = await _filmRepository.GetDirectorFilmsAsync(directorId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(films);
        }

        [HttpGet("GetIdFilms/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Film>))]
        public async Task<IActionResult> GetIdFilmsAsync(int id)
        {
            var films = await _filmRepository.GetIdFilmsAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(films);
        }

    }
}
