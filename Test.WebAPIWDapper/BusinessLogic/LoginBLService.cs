using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using WebAPIWDapper.Models;
using WebAPIWDapper.Services;
using Newtonsoft.Json;
namespace WebAPIWDapper.BusinessLogic
{
    public class LoginBLService : BusinessLogicBLBase
    {
        private readonly ILoginService _loginService;
        public LoginBLService(IConfiguration configuration) : base(configuration)
        {
            _loginService = new LoginService(_dbService);
        }
        public async Task<bool?> RegisterUser(Login user)
        {
            CryptographicService encryptionDecryptionService = new CryptographicService(_dbService);
            CryptographicBLService encryptionBLService = new CryptographicBLService(_configuration);
            string encryptionKeyJson = await encryptionBLService.GetRandomEncryptionKeyValueFromCache();
            EncryptionKey? encryptionKey = JsonConvert.DeserializeObject<EncryptionKey>(encryptionKeyJson);
            if (encryptionKey is null || encryptionKey.KeyString is null || user.Password is null)
            {
                return false;
            }
            user.EncryptionKeyId = encryptionKey.Id;
            user.Password = await encryptionDecryptionService.CreateHash(user.Password, encryptionKey.KeyString);
            var result = await _loginService.CreateLogin(user);
            return result;
        }

        public async Task<bool?> UpdateUser(Login user)
        {
            CryptographicService cryptographicService = new CryptographicService(_dbService);
            CryptographicBLService cryptographicBLService = new CryptographicBLService(_configuration);
            EncryptionKey? encryptionKey = null;
            string? encryptionKeyJson = await cryptographicBLService.GetEncryptionKeyValueFromCache(user.EncryptionKeyId);
            if (!string.IsNullOrEmpty(encryptionKeyJson))
            {
                encryptionKey = JsonConvert.DeserializeObject<EncryptionKey>(encryptionKeyJson);
            }
            user.Password = await cryptographicService.CreateHash(user.Password, encryptionKey.KeyString);
            var result = await _loginService.UpdateLogin(user);
            return true;
        }

        public async Task<Login>? GetLoginMatching(string userName)
        {
            var result = await _loginService.GetLogin(userName);
            return result;
        }

    }
}
