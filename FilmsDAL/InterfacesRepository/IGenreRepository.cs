using ApiDemoFilms.Model;

namespace Films.DAL.Interfaces
{
    public interface IGenreRepository
    {
        Task<Genre> GetIdGenresAsync(int id);
        Task<ICollection<Genre>> GetNameGenresAsync(string genreName);
        Task<ICollection<Genre>> GetGenresAsync();
    }
}
