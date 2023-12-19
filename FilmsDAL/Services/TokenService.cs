using ApiDemoFilms.Model;
using Films.DAL.Helpers;
using Films.DAL.Interfaces;
using Films.DAL.InterfacesServices;
using Films.DAL.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Films.DAL.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;

        private readonly IRefreshTokenRepository _tokenRepository;

        //IOptions - интерфейс для упрощения доступа к настройкам приложения
        public TokenService(IOptions<AppSettings> appSettings, ConnectionMultiplexer redis, IRefreshTokenRepository tokenRepository)
        {
            _appSettings = appSettings.Value;
            _tokenRepository = tokenRepository;
        }
        public async Task<TokenModel> GetRefreshTokenAsync(TokenModel tokenModel)
        {
            JwtSecurityToken jwtToken = CheckToken(tokenModel.Token, false);
            var userModel = ExtractUserFromToken(jwtToken);
            var checkResult = await CheckRefreshTokenAsync(userModel.NickName, tokenModel.RefreshToken);
            var oldRefreshToken = await _tokenRepository.GetRefreshTokenAsync(tokenModel.RefreshToken);

            if (checkResult == CheckRefreshTokenResult.Ok) 
            {
                oldRefreshToken.isUsed = true;
                await _tokenRepository.SetRefreshTokenAsync(oldRefreshToken);
                return await GenerateTokenAsync(userModel.NickName);
            }
            throw new AppException($"Refresh token is invalid {checkResult}"); //?? условие инвалид рефреш токена

        }

        public async Task<TokenModel> GenerateTokenAsync(string nickName)
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
                new Claim(JwtRegisteredClaimNames.Name, nickName),
            }),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.TokenValidityInMinutes),
                SigningCredentials = signingCredentials

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = await CreateRefreshToken(nickName);

            return new TokenModel { Token = tokenHandler.WriteToken(token), RefreshToken = refreshToken };
        }

        private async Task<string> CreateRefreshToken(string nickName)
        {
            var id = Guid.NewGuid().ToString();
            
            var refreshToken = new RefreshToken { NickName = nickName, isUsed = false, Expiration = DateTime.UtcNow.AddDays(_appSettings.RefreshTokenValidityInDays), refreshToken = id};
            var token = await _tokenRepository.SetRefreshTokenAsync(refreshToken);
            return await Task.FromResult(id); 
        }

        Task ITokenService.RevokeRefreshTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<User> ValidateTokenAsync(string token)
        {
            JwtSecurityToken jwtToken = CheckToken(token, true);
            var userModel = ExtractUserFromToken(jwtToken);

            return Task.FromResult(userModel);
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
            var nickName = jwtToken.Claims.First(x => x.Type == "name").Value;
            return new User
            {
                NickName = nickName
            };
        }
        private async Task<CheckRefreshTokenResult> CheckRefreshTokenAsync(string nickName, string refreshTokenId)
        {
            var refreshToken = await _tokenRepository.GetRefreshTokenAsync(refreshTokenId);

            if (refreshToken == null) return CheckRefreshTokenResult.NotFound;
            if (refreshToken.NickName != nickName) return CheckRefreshTokenResult.Incorrect;
            if (refreshToken.isUsed) return CheckRefreshTokenResult.IsUsed;
            if (refreshToken.Expiration < DateTime.UtcNow) return CheckRefreshTokenResult.Expired;
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

