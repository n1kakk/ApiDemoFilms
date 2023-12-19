using ApiDemoFilms.Model;
using Films.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Films.DAL.Repository
{
    public class FilmRepository: IFilmRepository
    {
        private readonly ApplicationDbContext _context;
        public FilmRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Film>> GetFilms()
        {
            return await _context.Films.OrderByDescending(y => y.ReleaseYear).Include(g=>g.Genre).Include(d=>d.Director).ToListAsync();
        }

        public async Task<ICollection<Film>> GetGenreFilmsAsync(string genre)
        {
            return await _context.Films.Include(g => g.Genre).Where(g => g.Genre.GenreName == genre).ToListAsync();
        }

        public async Task<ICollection<Film>> GetDirectorFilmsAsync(int directorId)
        {
            return await _context.Films.Include(d => d.Director).Where(d=>d.Director.Id == directorId).ToListAsync();
        }

        public async Task<Film> GetIdFilmsAsync(int id)
        {
            return await _context.Films.Where(f => f.Id == id).Include(g=>g.Genre).Include(d => d.Director).FirstAsync();
        }

        public async Task<ICollection<Film>> GetReleaseYearFilmsAsync(int releaseYear)
        {
            return await _context.Films.Where(y => y.ReleaseYear == releaseYear).Include(a => a.Genre).Include(d => d.Director).ToListAsync();
        }
    }
}
