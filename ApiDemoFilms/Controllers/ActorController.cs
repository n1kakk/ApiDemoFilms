using ApiDemoFilms.Model;
using Films.DAL.Interfaces;
using Films.DAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemoFilms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ActorController: Controller
    {
        private readonly IActorRepository _actorRepository;
        public ActorController (IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        [HttpGet("GetIdActors/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Actor>))]
        public async Task<IActionResult> GetIdActorsAsync(int id)
        {
            var films = await _actorRepository.GetIdActorsAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(films);
        }

        [HttpGet("GetActors")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Actor>))]
        public async Task<IActionResult> GetActorsAsync()
        {
            var films = await _actorRepository.GetActorsAsync();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(films);
        }

    }
}
