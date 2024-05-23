using System.Security.Cryptography;
using System.Text;
using WebAPIWDapper.Models;
using static System.Net.Mime.MediaTypeNames;



namespace WebAPIWDapper.Services;

public class EncryptionKeysService : IEncryptionKeysService
{
    private readonly IDbService _dbService;

    public EncryptionKeysService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<List<EncryptionKey>> GetAllActiveKeys()
    {
        List<EncryptionKey> EncryptionKeyList = await _dbService.GetAll<EncryptionKey>("SELECT * FROM public.EncryptionKeys WHERE ExpiryDate>=now()");
        return EncryptionKeyList;
    }
    public async Task<bool> AddKey(EncryptionKey encryptionKey)
    {
        var result =
            await _dbService.EditData(
                "INSERT INTO public.EncryptionKeys (KeyString, ExpiryDate) VALUES (@keyString, @expiryDate)",
                encryptionKey);
        return true;
    }
    public async Task<bool> ExpireKey(EncryptionKey encryptionKey)
    {
        return true;
    }
    //public async Task<bool> ReplaceKeys(EncryptionKey expiringEncryptionKey)
    //{
    //    return true;
    //}
}