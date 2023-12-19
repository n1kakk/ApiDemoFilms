using ApiDemoFilms.Model;
using Films.DAL.Interfaces;
using Films.DAL.InterfacesServices;

namespace Films.DAL.Services
{
    public class ActorService: IActorService
    {
        private readonly IActorRepository _actorRepository;
        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<ICollection<Actor>> GetActorsAsync()
        {
            var actors = await _actorRepository.GetActorsAsync();
            return actors;
        }

        public async Task<Actor> GetIdActorsAsync(int id)
        {
            var actor = await _actorRepository.GetIdActorsAsync(id);
            return actor;
        }
    }
}
