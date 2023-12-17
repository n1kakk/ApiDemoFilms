
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

        public Task<TokenModel> DeleteRefreshTokenAsync(string nickName)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenModel> GetRefreshTokenAsync(string nickName)
        {
            var key = keyPrefix + nickName;
            bool refreshTokenExists = await _database.KeyExistsAsync(key);
            if (!refreshTokenExists) return null;

            var data = await _database.StringGetAsync(key);

            var token = JsonSerializer.Deserialize<TokenModel>(data);

            return token;
        }

        public async Task<TokenModel> SetRefreshTokenAsync(Task<TokenModel> tokenModel, string nickName)
        {
            var key = keyPrefix + nickName;
            bool refreshTokenExists = await _database.KeyExistsAsync(key);


            await _database.StringSetAsync(key, JsonSerializer.Serialize(tokenModel));

            return await tokenModel;
        }

    }
}





