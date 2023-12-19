using ApiDemoFilms.Model;
using Films.DAL.Model;

namespace Films.DAL.InterfacesServices
{
    public interface ITokenService
    {
        public Task<TokenModel> GetRefreshTokenAsync(TokenModel tokenModel);
        public Task<TokenModel> GenerateTokenAsync(string nickName);
        public Task RevokeRefreshTokenAsync(string token);
        public Task<User> ValidateTokenAsync(string token); //почему User??
    }

}
