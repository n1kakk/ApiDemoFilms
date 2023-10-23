using Microsoft.EntityFrameworkCore;
using ApiDemoFilms.Model;
using Films.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Films.DAL.Repository
{
    public class DirectorRepository: IDirectorRepository
    {
        private readonly ApplicationDbContext _context;
        public DirectorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Director>> GetIdDirectorsAsync(int id)
        {
            return await _context.Directors.Where(d => d.Id == id).ToListAsync();
        }
        public async Task<ICollection<Director>> GetDirectorsAsync()
        {
            return await _context.Directors.ToListAsync();
        }

    }
}
