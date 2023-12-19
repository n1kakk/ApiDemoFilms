using ApiDemoFilms.Model;

namespace Films.DAL.Interfaces
{
    public interface IDirectorRepository
    {
        Task<Director> GetIdDirectorsAsync(int id);
        Task<ICollection<Director>> GetDirectorsAsync();
    }
}
