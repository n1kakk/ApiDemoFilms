using Films.DAL.Model;

namespace Films.DAL.Interfaces
{
    public interface IRefreshTokenRepository
    {
        public Task<TokenModel> GetRefreshTokenAsync(string nickName);
        public Task<TokenModel> SetRefreshTokenAsync(Task<TokenModel> tokenModel, string nickName );
        public Task<TokenModel> DeleteRefreshTokenAsync(string nickName);
    }
}
