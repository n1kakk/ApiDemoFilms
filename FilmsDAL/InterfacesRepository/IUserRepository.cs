using ApiDemoFilms.Model;

namespace Films.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetIdUsersAsync(int id);
        Task<User?> GetNickNameUsersAsync(string nickName);
        Task SetUserAsync(User user);
        
    }
}
