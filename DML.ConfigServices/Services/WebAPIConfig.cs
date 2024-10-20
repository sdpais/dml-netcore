using DML.ConfigServices.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace DML.ConfigServices.Services;

public class WebAPIConfig : IWebAPIConfig
{
    public string RedisConnectionString
    {
        get
        {
            
            var redisURL = ApplicationConfiguration.GetSetting("Redis:URL");
            var redisDbPassword = ApplicationConfiguration.GetSetting("Redis:Password");
            return $"{redisURL},password={redisDbPassword},ssl=False,abortConnect=False";
        }
    }

    public string EmployeePGConnectionString
    {
        get
        {
            var employeedbConnection = ApplicationConfiguration.GetSetting("ConnectionStrings:Employeedb");
            
            return employeedbConnection;
        }
    }
    public string RBACPGConnectionString
    {
        get
        {

            var rBACdbConnection = ApplicationConfiguration.GetSetting("ConnectionStrings:RBACdb");

            return rBACdbConnection;
        }
    }

    public SymmetricSecurityKey JWTSecretKey {
        get
        {
            string secretValue = ApplicationConfiguration.GetSetting("JWT:Secret");
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
