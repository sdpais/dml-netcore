using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebAPIWDapper.Models;
using WebAPIWDapper.Services;

namespace WebAPIWDapper.BusinessLogic
{
    public class JWTBLService : BusinessLogicBLBase
    {
        public JWTBLService(IConfiguration _configuration) : base(_configuration)
        {
        }
        public async Task<JWTToken> GenerateToken(string username)
        {
            // get user details
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Secret") ?? string.Empty));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(issuer: _configuration.GetValue<string>("JWT:ValidIssuer"), audience: _configuration.GetValue<string>("JWT:ValidAudience"), claims: new List<Claim>(), expires: DateTime.Now.AddMinutes(6), signingCredentials: signinCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            var refreshToken = "";
            DateTime accessTokenExpiry = DateTime.Now.AddMinutes(15);
            DateTime refreshTokenExpiry = DateTime.Now.AddDays(7);
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken = Convert.ToBase64String(randomNumber);
            }
            
            return new JWTToken
            {
                AccessToken = tokenString,
                AccessTokenExpiry = accessTokenExpiry,
                RefreshToken = refreshToken,
                RefreshTokenExpiry = refreshTokenExpiry
            };
        }


    }
}
