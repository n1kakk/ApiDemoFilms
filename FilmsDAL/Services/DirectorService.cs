using ApiDemoFilms.Model;
using Films.DAL.Interfaces;
using Films.DAL.InterfacesServices;

namespace Films.DAL.Services
{
    public class DirectorService: IDirectorService
    {
        private readonly IDirectorRepository _directorRepository;

        public DirectorService(IDirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }

        public async Task<ICollection<Director>> GetDirectorsAsync()
        {
            var directors = await _directorRepository.GetDirectorsAsync();
            return directors;
        }

        public async Task<Director> GetIdDirectorsAsync(int id)
        {
            var director = await _directorRepository.GetIdDirectorsAsync(id);
            return director;
        }
    }
}
