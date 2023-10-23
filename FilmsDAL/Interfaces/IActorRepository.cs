using ApiDemoFilms.Model;

namespace Films.DAL.Interfaces
{
    public interface IActorRepository
    {
        Task<ICollection<Actor>> GetIdActorsAsync(int id);
        Task<ICollection<Actor>> GetActorsAsync();

    }
}
