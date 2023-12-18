
using Films.DAL.Interfaces;
using Films.DAL.Model;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System.Text.Json;

namespace Films.DAL.Repository
{
    public class TokenRepository : IRefreshTokenRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly StackExchange.Redis.IDatabase _database;
        private readonly string keyPrefix = "refreshToken:";
        private readonly string keyPrefixAccessToken = "accessToken: ";

        public TokenRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<RefreshToken> DeleteRefreshTokenAsync(string refreshTokenId)
        {
            var key = keyPrefix + refreshTokenId;
            
            bool refreshTokenExists = await _database.KeyExistsAsync(key);
            if (!refreshTokenExists) return null;
            var data = await _database.StringGetAsync(key);

            var token = JsonSerializer.Deserialize<RefreshToken>(data);
            await _database.KeyDeleteAsync(key);

            return token;
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(string refreshTokenId)
        {
            var key = keyPrefix + refreshTokenId;
            bool refreshTokenExists = await _database.KeyExistsAsync(key);
            if (!refreshTokenExists) return null;

            var data = await _database.StringGetAsync(key);

            var token = JsonSerializer.Deserialize<RefreshToken>(data);

            return token;
        }

        public async Task<RefreshToken> SetRefreshTokenAsync(RefreshToken refreshToken)
        {
            var key = keyPrefix + refreshToken.refreshToken;
            bool refreshTokenExists = await _database.KeyExistsAsync(key);
            if(!refreshTokenExists) await _database.StringSetAsync(key, JsonSerializer.Serialize(refreshToken));

            return refreshToken;
        }

    }
}





