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
    public class UserBLService : BusinessLogicBLBase
    {
        public UserBLService(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<bool?> RegisterUser(Login user)
        {
            ILoginService _loginService = new LoginService(_dbService);
            CryptographicService encryptionDecryptionService = new CryptographicService(_dbService);
            EncryptionBLService encryptionBLService = new EncryptionBLService(_configuration);
            string encryptionKeyJson = await encryptionBLService.GetRandomEncryptionKeyValueFromCache();
            EncryptionKey? encryptionKey = JsonConvert.DeserializeObject<EncryptionKey>(encryptionKeyJson);
            if (encryptionKey is null || encryptionKey.KeyString is null || user.Password is null)
            {
                return false;
            }
            user.EncryptionKeyId = encryptionKey.Id;
            user.Password = await encryptionDecryptionService.Encrypt(user.Password, encryptionKey.KeyString);
            var result = await _loginService.CreateLogin(user);
            return result;
        }

    }
}
