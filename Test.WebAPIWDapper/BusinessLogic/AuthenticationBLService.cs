using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using WebAPIWDapper.Models;
using WebAPIWDapper.Services;

namespace WebAPIWDapper.BusinessLogic
{
    public class AuthenticationBLService : BusinessLogicBLBase
    {
        public AuthenticationBLService(IConfiguration configuration) : base(configuration)
        {
            
        }
        public async Task<JWTToken?> AuthenticateUser(Login user)
        {
            ILoginService loginService = new LoginService(_dbService);
            Login dbUser = await loginService.GetLogin(user.UserName);
            if (dbUser is not null && dbUser.Password == user.Password)
            {
                JWTBLService jWTBLService = new JWTBLService(_configuration);
                JWTToken userToken = await jWTBLService.GenerateToken(user.UserName);
                return userToken;

            }
            throw new AuthenticationException("Invalid username or password.");
        }
    }

    }
}
