using ApiDemoFilms.Model;

namespace Films.DAL.Interfaces
{
    public interface IDirectorRepository
    {
        Task<ICollection<Director>> GetIdDirectorsAsync(int id);
        Task<ICollection<Director>> GetDirectorsAsync();
    }
}
