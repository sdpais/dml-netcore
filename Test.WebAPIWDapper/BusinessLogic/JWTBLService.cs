using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            
            return new JWTToken
            {
                Token = tokenString
            };
        }
    }
}
