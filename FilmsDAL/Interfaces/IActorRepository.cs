using ApiDemoFilms.Model;

namespace Films.DAL.Interfaces
{
    public interface IActorRepository
    {
        Task<Actor> GetIdActorsAsync(int id);
        Task<ICollection<Actor>> GetActorsAsync();

    }
}
