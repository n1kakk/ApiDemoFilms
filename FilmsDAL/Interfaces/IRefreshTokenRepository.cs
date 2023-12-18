using Films.DAL.Model;

namespace Films.DAL.Interfaces
{
    public interface IRefreshTokenRepository
    {
        public Task<RefreshToken> GetRefreshTokenAsync(string refreshTokenId);
        public Task<RefreshToken> SetRefreshTokenAsync(RefreshToken refreshToken);
        public Task<RefreshToken> DeleteRefreshTokenAsync(string refreshTokenId);
    }
}
