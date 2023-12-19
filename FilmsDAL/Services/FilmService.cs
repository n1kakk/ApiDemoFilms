using ApiDemoFilms.Model;
using Films.DAL.Interfaces;
using Films.DAL.InterfacesServices;

namespace Films.DAL.Services
{
    public class FilmService: IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        public FilmService(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository; 
        }

        public async Task<ICollection<Film>> GetDirectorFilmsAsync(int directorId)
        {
            var films = await _filmRepository.GetDirectorFilmsAsync(directorId);
            return films;
        }

        public async Task<ICollection<Film>> GetFilms()
        {
            var films =await  _filmRepository.GetFilms();
            return films;
        }

        public async Task<ICollection<Film>> GetGenreFilmsAsync(string genre)
        {
            var films = await _filmRepository.GetGenreFilmsAsync(genre);
            return films;
        }

        public async Task<Film> GetIdFilmsAsync(int id)
        {
            var film = await _filmRepository.GetIdFilmsAsync(id);
            return film;
        }

        public async Task<ICollection<Film>> GetReleaseYearFilmsAsync(int releaseYear)
        {
            var films = await _filmRepository.GetReleaseYearFilmsAsync(releaseYear);
            return films;
        }
    }
}
