using System.Security.Cryptography;
using System.Text;
using WebAPIWDapper.Models;
using static System.Net.Mime.MediaTypeNames;



namespace WebAPIWDapper.Services;

public class EncryptionDecryptionService : IEncryptionDecryptionService
{
    private readonly IDbService _dbService;

    public EncryptionDecryptionService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public Task<string> Encrypt(string unencryptedText, string keyString)
    {
        var key = Encoding.UTF8.GetBytes(keyString);

        using (var aesAlg = Aes.Create())
        {
            using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
            {
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(unencryptedText);
                    }

                    var iv = aesAlg.IV;

                    var decryptedContent = msEncrypt.ToArray();

                    var result = new byte[iv.Length + decryptedContent.Length];

                    Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                    Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);
                    return Task.FromResult(Convert.ToBase64String(result));
                }
            }
        }
    }
    public Task<string> Decrypt(string encryptedText, string keyString)
    {
        var fullCipher = Convert.FromBase64String(encryptedText);

        var iv = new byte[16];
        var cipher = new byte[16];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
        var key = Encoding.UTF8.GetBytes(keyString);

        using (var aesAlg = Aes.Create())
        {
            using (var decryptor = aesAlg.CreateDecryptor(key, iv))
            {
                string result;
                using (var msDecrypt = new MemoryStream(cipher))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            result = srDecrypt.ReadToEnd();
                        }
                    }
                }

                return Task.FromResult(result);
            }
        }
    }
}