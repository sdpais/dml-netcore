using WebAPIWDapper.Models;


namespace WebAPIWDapper.Services;

public interface IEncryptionDecryptionService
{
    Task<string> Encrypt(string unencrypted, string keyString);
    Task<string> Decrypt(string encrypted, string keyString);
    //Task<List<string>> ReEncrypt(string encrypted, string oldKey, string newKey);
    //Task<bool> GenerateNewKey(string key);
    
}