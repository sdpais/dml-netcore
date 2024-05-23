using WebAPIWDapper.Models;


namespace WebAPIWDapper.Services;

public interface ICryptographicService
{
    Task<string> Encrypt(string unencrypted, string keyString);
    Task<string> Encrypt(string unencrypted, byte[] keyArray);

    Task<string> Decrypt(string encrypted, string keyString);
    Task<string> Decrypt(string encrypted, byte[] keyArray);
    //Task<List<string>> ReEncrypt(string encrypted, string oldKey, string newKey);
    //Task<bool> GenerateNewKey(string key);

}