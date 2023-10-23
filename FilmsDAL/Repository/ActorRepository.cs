using Microsoft.EntityFrameworkCore;
using ApiDemoFilms.Model;
using Films.DAL.Interfaces;

namespace Films.DAL.Repository
{
    public class ActorRepository : IActorRepository
    {
        private readonly ApplicationDbContext _context;
        public ActorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Actor>> GetIdActorsAsync(int id)
        {
            return await _context.Actors.Where(a => a.Id == id).ToListAsync();
        }
        public async Task<ICollection<Actor>> GetActorsAsync()
        {
            return await _context.Actors.Take(5).ToListAsync();
        }
    }

}
