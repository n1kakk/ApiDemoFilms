using ApiDemoFilms.Model;
using Films.DAL.Helpers;
using Films.DAL.Interfaces;
using Films.DAL.Model;
using Films.DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace ApiDemoFilms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("GetIdUsers/{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public async Task<IActionResult> GetIdUsersAsync(int id)
        {
            var user = await _userRepository.GetIdUsersAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }

        [HttpGet("GetNickNameUser/{nickName}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public async Task<IActionResult> GetNickNameUsers(string nickName)
        {
            var user = await _userRepository.GetNickNameUsersAsync(nickName);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> Signup(SignupRequest model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userRepository.GetNickNameUsersAsync(model.NickName);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "A user with this nickname already exists");
                    return BadRequest(ModelState);
                }
                string salt = PasswordHasher.Salt();
                string hashedPassword = PasswordHasher.HashPassword(model.Password+salt);
                var user = new User
                {
                    NickName = model.NickName,
                    Password = hashedPassword,
                    Salt = salt,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Birthday = model.Birthday,
                };

                await _userRepository.SignupAsync(user);
                return Created("",user);
            }
                return BadRequest(ModelState); 
        }


        [HttpPost("LogIn")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userRepository.GetNickNameUsersAsync(model.NickName);
                if (existingUser == null)
                {
                    ModelState.AddModelError(string.Empty, "You need to sign up");
                    return BadRequest(ModelState);
                }
                if (existingUser != null && PasswordHasher.VerifyPassword(model.Password, existingUser.Password, existingUser.Salt))
                {
                    return Ok();
                }
            }
            return Unauthorized();
        }
    }
}
