using ApiDemoFilms.Model;
using Films.DAL.Model;

namespace Films.DAL.InterfacesServices
{
    public interface IUserService
    {
        public Task<User> GetIdUsersAsync(int id);
        public Task<User> GetNickNameUsersAsync(string nickName);
        public Task<User> Signup(SignupRequest model);
      
    }
}
