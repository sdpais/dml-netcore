using Microsoft.IdentityModel.Tokens;

namespace DML.ConfigServices.Interfaces
{
    public interface IWebAPIConfig
    {
        public string RedisConnectionString { get; }

        public string RBACPGConnectionString { get; }
        public SymmetricSecurityKey JWTSecretKey { get; }
        public string SignInCredentials { get; }

        public string TokenOptions { get; }
        public string TokenString { get; }




    }
}
