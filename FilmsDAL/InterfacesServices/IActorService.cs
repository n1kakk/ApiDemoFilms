using ApiDemoFilms.Model;

namespace Films.DAL.InterfacesServices
{
    public interface IActorService
    {
        public Task<Actor> GetIdActorsAsync(int id);
        public Task<ICollection<Actor>> GetActorsAsync();

    }
}
