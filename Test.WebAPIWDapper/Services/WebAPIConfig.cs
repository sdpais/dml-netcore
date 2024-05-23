using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Eventing.Reader;
using System.Text;

namespace WebAPIWDapper.Services
{
    public class WebAPIConfig : IWebAPIConfig
    {
        private IConfiguration _configuration { get; }
        public WebAPIConfig(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public string RedisConnectionString
        {
            get
            {
                if (_configuration is null)
                {
                    throw new ArgumentNullException(nameof(_configuration));
                }
                var redisURL = _configuration.GetValue<string>("Redis:URL");
                var redisDbPassword = (_configuration.GetValue<string>("Redis:Password"));
                return $"{redisURL},password={redisDbPassword},ssl=False,abortConnect=False";
            }
        }

        public SymmetricSecurityKey JWTSecretKey {
            get
            {
                if (_configuration is null)
                {
                    throw new ArgumentNullException(nameof(_configuration));
                }
                string secretValue = _configuration.GetValue<string>("JWT:Secret");
                if (secretValue is null)
                {
                    throw new Exception("Configuration is invalid for JWT settings");
                }
                else
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretValue));
                    return secretKey;
                }
                
            }
        }
        //TODO
        public string SignInCredentials { get; }

        public string TokenOptions { get; }
        public string TokenString { get; }

    }
}
