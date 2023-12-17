using ApiDemoFilms.Model;
using Films.DAL.Helpers;
using Films.DAL.Interfaces;
using Films.DAL.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Films.DAL.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;
        private readonly ConnectionMultiplexer _redis;
        private readonly StackExchange.Redis.IDatabase _database;
        private readonly string keyPrefix = "refreshToken:";

        private readonly IRefreshTokenRepository _tokenRepository;

        //private readonly ConcurrentDictionary<string, RefreshTokenInfo> refreshTokens = new ConcurrentDictionary<string, RefreshTokenInfo>();
        //private readonly ILogger _logger;

        //IOptions - интерфейс для упрощения доступа к настройкам приложения
        public TokenService(IOptions<AppSettings> appSettings, ConnectionMultiplexer redis, IRefreshTokenRepository tokenRepository)
        {
            _appSettings = appSettings.Value;
            _redis = redis;
            _database = redis.GetDatabase();

            _tokenRepository = tokenRepository;
        }
        public async Task<TokenModel> GetRefreshTokenAsync(TokenModel tokenModel)
        {
            JwtSecurityToken jwtToken = CheckToken(tokenModel.Token, false);
            var userModel = ExtractUserFromToken(jwtToken);
            var checkResult = CheckRefreshToken(userModel.NickName, tokenModel.RefreshToken);

            var key = keyPrefix + userModel.NickName;
            var data = _database.StringGet(key);
            var refreshTokens = JsonSerializer.Deserialize<RefreshToken>(data);

            if (checkResult == CheckRefreshTokenResult.Ok) 
            { 
                refreshTokens.isUsed = true;
                Console.WriteLine(userModel.NickName);
                return await GenerateTokenAsync(userModel);
            }
            throw new AppException($"Refresh token is invalid {checkResult}"); //?? условие инвалид рефреш токена

        }

        public async Task<TokenModel> GenerateTokenAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SigningCredentials signingCredentials = null;
            if (!string.IsNullOrEmpty(_appSettings.JWTSecret))
            {
                var key = Encoding.ASCII.GetBytes(_appSettings.JWTSecret);
                signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            }


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                //new Claim("NickName", user.NickName.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.NickName),
            }),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.TokenValidityInMinutes),
                SigningCredentials = signingCredentials

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = await CreateRefreshToken(user);

            return new TokenModel { Token = tokenHandler.WriteToken(token), RefreshToken = refreshToken };
        }

        private async Task<string> CreateRefreshToken(User user)
        {
            var id = Guid.NewGuid().ToString();
            //refreshTokens.TryAdd(id, new RefreshToken { NickName = user.NickName, isUsed = false, Expiration = DateTime.UtcNow.AddDays(_appSettings.RefreshTokenValidityInDays) });
            //return Task.FromResult(id);
            var key = keyPrefix + user.NickName;
            bool refreshTokenExists = await _database.KeyExistsAsync(key);
            if (!refreshTokenExists) { var refreshToken = new RefreshToken { NickName = user.NickName, isUsed = false, Expiration = DateTime.UtcNow.AddDays(_appSettings.RefreshTokenValidityInDays) }; };
            //var token = await _tokenRepository.SetRefreshTokenAsync()
            return await Task.FromResult(id); //???
        }

        Task ITokenService.RevokeRefreshTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        User ITokenService.ValidateToken(string token)
        {
            throw new NotImplementedException();
        }

        private JwtSecurityToken CheckToken(string token, bool validateLifetime)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.JWTSecret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = validateLifetime,
                    //ClockSkew = TimeSpan.Zero //sets it to zero
                }, out SecurityToken validatedToken);
                return (JwtSecurityToken)validatedToken; //приведение validatedToken к типу JwtSecurityToken
            }
            catch (Exception ex)
            {
                throw new AppException($"Token is invalid {ex.Message}");
            }
        }

        private User ExtractUserFromToken(JwtSecurityToken jwtToken)
        {
            var nickName = jwtToken.Claims.First(x => x.Type == "nickName").Value;
            return new User
            {
                NickName = nickName
            };
        }
        private CheckRefreshTokenResult CheckRefreshToken(string nickName, string refreshToken)
        {
            var key = keyPrefix + nickName;
            bool refreshTokenExists = _database.KeyExists(key);

            var data = _database.StringGet(key);
            var refreshTokens = JsonSerializer.Deserialize<RefreshToken>(data);

            if (!refreshTokenExists) return CheckRefreshTokenResult.NotFound;
            if (refreshTokens.NickName != nickName) return CheckRefreshTokenResult.Incorrect;
            if (refreshTokens.isUsed) return CheckRefreshTokenResult.IsUsed;
            if (refreshTokens.Expiration < DateTime.UtcNow) return CheckRefreshTokenResult.Expired;
            return CheckRefreshTokenResult.Ok;
            
        }

        enum CheckRefreshTokenResult
        {
            Ok,
            NotFound,
            Expired,
            IsUsed,
            Incorrect
        }

    }
}

