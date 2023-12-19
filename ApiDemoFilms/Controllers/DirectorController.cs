using ApiDemoFilms.Model;
using Films.DAL.InterfacesServices;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemoFilms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController: Controller
    {
        private readonly IDirectorService _directorService;

        public DirectorController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        [HttpGet("GetIdDirectors/{id}")]
        [ProducesResponseType(200, Type = typeof(Director))]
        public async Task<IActionResult> GetIdDirectorsAsync(int id)
        {
            var director = await _directorService.GetIdDirectorsAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(director);
        }

        [HttpGet("GetDirectors")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Director>))]
        public async Task<IActionResult> GetDirectorsAsync()
        {
            var directors = await _directorService.GetDirectorsAsync();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(directors);
        }

    }
}
