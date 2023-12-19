using ApiDemoFilms.Model;
using Films.DAL.Interfaces;
using Films.DAL.InterfacesServices;

namespace Films.DAL.Services
{
    public class GenreService: IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<ICollection<Genre>> GetGenresAsync()
        {
            var genres = await _genreRepository.GetGenresAsync();
            return genres;
        }

        public async Task<Genre> GetIdGenresAsync(int id)
        {
            var genre = await _genreRepository.GetIdGenresAsync(id);
            return genre;
        }

        public async Task<ICollection<Genre>> GetNameGenresAsync(string genreName)
        {
            var genres = await _genreRepository.GetNameGenresAsync(genreName);
            return genres;
        }
    }
}
