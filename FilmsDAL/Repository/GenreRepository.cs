using ApiDemoFilms.Model;
using Films.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Films.DAL.Repository
{
    public class GenreRepository: IGenreRepository
    {
        private readonly ApplicationDbContext _context;
        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> GetIdGenresAsync(int id)
        {
            return await _context.Genres.Where(g => g.Id == id).FirstAsync();
        }
        public async Task<ICollection<Genre>> GetNameGenresAsync(string genreName)
        {
            return await _context.Genres.Where(g => g.GenreName == genreName).ToListAsync();
        }

        public async Task<ICollection<Genre>> GetGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }
    }
}
