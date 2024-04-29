using Microsoft.IdentityModel.Tokens;

namespace Test.WebAPIWDapper.Services
{
    public interface IWebAPIConfig
    {
        public string RedisConnectionString { get; }
        public SymmetricSecurityKey JWTSecretKey { get; }
        public string SignInCredentials { get; }

        public string TokenOptions { get; }
        public string TokenString { get; }




    }
}
