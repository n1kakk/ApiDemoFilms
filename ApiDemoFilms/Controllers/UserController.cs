using ApiDemoFilms.Model;
using Films.DAL.Helpers;
using Films.DAL.InterfacesServices;
using Films.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemoFilms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpGet("GetIdUsers/{id}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(User))]
        public async Task<IActionResult> GetIdUsersAsync(int id)
        {
            var user = await _userService.GetIdUsersAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }

        [HttpGet("GetNickNameUser/{nickName}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public async Task<IActionResult> GetNickNameUsers(string nickName)
        {
            var user = await _userService.GetNickNameUsersAsync(nickName);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> Signup(SignupRequest model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userService.GetNickNameUsersAsync(model.NickName);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "A user with this nickname already exists");
                    return BadRequest(ModelState);
                }
                var user = await _userService.Signup(model);
                var tokenModel = _tokenService.GenerateTokenAsync(user.NickName);

                return Ok((user, tokenModel));
            }
                return BadRequest(ModelState); 
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userService.GetNickNameUsersAsync(model.NickName);
                if (existingUser == null)
                {
                    ModelState.AddModelError(string.Empty, "You need to sign up");
                    return BadRequest(ModelState);
                }
                if (existingUser != null && PasswordHasher.VerifyPassword(model.Password, existingUser.Password, existingUser.Salt))
                {
                    var tokenModel =await _tokenService.GenerateTokenAsync(existingUser.NickName);
                    return Ok((tokenModel));
                }
            }
            return Unauthorized();
        }

        [HttpPost("GetRefreshToken")]
        public async Task<IActionResult> GetRefreshToken(TokenModel tokenModel)
        {
            var tokenModelResponse = await _tokenService.GetRefreshTokenAsync(tokenModel);

            return Ok(tokenModelResponse);
        }

    }
}
