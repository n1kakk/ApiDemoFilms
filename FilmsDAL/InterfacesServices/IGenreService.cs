
using ApiDemoFilms.Model;

namespace Films.DAL.InterfacesServices
{
    public interface IGenreService
    {
        public Task<Genre> GetIdGenresAsync(int id);
        public Task<ICollection<Genre>> GetNameGenresAsync(string genreName);
        public Task<ICollection<Genre>> GetGenresAsync();
    }
}
