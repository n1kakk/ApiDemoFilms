using Microsoft.EntityFrameworkCore;
using ApiDemoFilms.Model;
using Films.DAL.Interfaces;
using Films.DAL.Model;

namespace Films.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<User>> GetIdUsersAsync(int id)
        {
            return await _context.Users.Where(u => u.Id == id).ToListAsync();
        }

        public async Task <User> GetNickNameUsersAsync(string nickName)
        {
            return await _context.Users.Where(u => u.NickName == nickName).FirstOrDefaultAsync();
        }

        public async Task SetUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }


    }
}
