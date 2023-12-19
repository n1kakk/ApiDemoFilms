using ApiDemoFilms.Model;

namespace Films.DAL.InterfacesServices
{
    public interface IFilmService
    {
        public Task<ICollection<Film>> GetFilms();
        public Task<ICollection<Film>> GetGenreFilmsAsync(string genre);
        public Task<ICollection<Film>> GetReleaseYearFilmsAsync(int releaseYear);
        public Task<ICollection<Film>> GetDirectorFilmsAsync(int directorId);
        public Task<Film> GetIdFilmsAsync(int id);
    }
}
