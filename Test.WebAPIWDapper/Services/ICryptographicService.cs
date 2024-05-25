using WebAPIWDapper.Models;


namespace WebAPIWDapper.Services;

public interface ICryptographicService
{
    Task<string> Encrypt(string unencrypted, string keyString);
    

    Task<string> Decrypt(string encrypted, string keyString);
    
    Task<string> CreateHash(string unencrypted, string keyString);
    Task<bool> CompareHash(string unencrypted, string keyString, string hashedValue);

    #region old code delete
    //Task<string> Encrypt(string unencrypted, byte[] keyArray);
    //Task<string> Decrypt(string encrypted, byte[] keyArray);
    #endregion
}