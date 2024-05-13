using WebAPIWDapper.Models;


namespace WebAPIWDapper.Services;

public interface IEncryptionKeysService
{
    Task<List<EncryptionKey>> GetAllActiveKeys();
    Task<bool> AddKey(EncryptionKey encryptionKey);
    Task<bool> ExpireKey(EncryptionKey encryptionKey);

    //Task<bool> ReplaceKeys(EncryptionKey expiringEncryptionKey);

    //Task<List<string>> ReEncrypt(string encrypted, string oldKey, string newKey);
    //Task<bool> GenerateNewKey(string key);

}