using ApiDemoFilms.Model;
using Films.DAL.InterfacesServices;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemoFilms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ActorController: Controller
    {
        private readonly IActorService _actorService;
        public ActorController (IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet("GetIdActors/{id}")]
        [ProducesResponseType(200, Type = typeof(Actor))]
        public async Task<IActionResult> GetIdActorsAsync(int id)
        {
            var actor = await _actorService.GetIdActorsAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(actor);
        }

        [HttpGet("GetActors")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Actor>))]
        public async Task<IActionResult> GetActorsAsync()
        {
            var actors = await _actorService.GetActorsAsync();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(actors);
        }

    }
}
