using ApiDemoFilms.Model;
using Films.DAL.Model;

namespace Films.DAL.Interfaces
{
    public interface ITokenService
    {
        public Task<TokenModel> GetRefreshTokenAsync(TokenModel tokenModel);
        public Task<TokenModel> GenerateTokenAsync(User user); 
        public Task RevokeRefreshTokenAsync(string token);
        public Task<User> ValidateTokenAsync(string token); //почему User??
    }

}
