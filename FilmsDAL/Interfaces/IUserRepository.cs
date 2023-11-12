using ApiDemoFilms.Model;
using Films.DAL.Model;

namespace Films.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetIdUsersAsync(int id);
        Task<User> GetNickNameUsersAsync(string nickName);
        //Task SignupAsync(SignupRequest user);
        Task SignupAsync(User user);

        //Task<User> LoginAsync(User user);
        
    }
}
