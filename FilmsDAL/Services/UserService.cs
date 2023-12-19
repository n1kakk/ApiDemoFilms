using ApiDemoFilms.Model;
using Films.DAL.Helpers;
using Films.DAL.Interfaces;
using Films.DAL.InterfacesServices;
using Films.DAL.Model;

namespace Films.DAL.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;       
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> GetIdUsersAsync(int id)
        {
            var user = await _userRepository.GetIdUsersAsync(id);
            return user;
        }
        public async Task<User> GetNickNameUsersAsync(string nickName)
        {
            var user = await _userRepository.GetNickNameUsersAsync(nickName);
            return user;
        }

        public async Task<User> Signup(SignupRequest model)
        {
            //var existingUser = await _userRepository.GetNickNameUsersAsync(model.NickName);

            string salt = PasswordHasher.Salt();
            string hashedPassword = PasswordHasher.HashPassword(model.Password + salt);

            var user = new User
            {
                NickName = model.NickName,
                Password = hashedPassword,
                Salt = salt,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Birthday = model.Birthday,
            };

            await _userRepository.SetUserAsync(user);
            return user;
        }
    }
}
