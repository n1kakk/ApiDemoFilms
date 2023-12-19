using ApiDemoFilms.Model;

namespace Films.DAL.Interfaces
{
    public interface IFilmRepository
    {
        Task<ICollection<Film>> GetFilms();
        Task<ICollection<Film>> GetGenreFilmsAsync(string genre);
        Task<Film> GetIdFilmsAsync(int id);
        Task<ICollection<Film>> GetReleaseYearFilmsAsync(int releaseYear);
        Task<ICollection<Film>> GetDirectorFilmsAsync(int directorId);

    }
}
