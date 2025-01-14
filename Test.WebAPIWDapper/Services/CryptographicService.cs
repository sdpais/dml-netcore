using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using WebAPIWDapper.Models;
using static System.Net.Mime.MediaTypeNames;



namespace WebAPIWDapper.Services;

public class CryptographicService : ICryptographicService
{
    private readonly IDbService _dbService;

    public CryptographicService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public Task<string> Encrypt(string value, string KeyValue)
    {
        if (string.IsNullOrEmpty(value)) return Task.FromResult(value);
        try
        {
            var key = Encoding.UTF8.GetBytes(KeyValue);

            using (var aesAlg = Aes.Create())
            {
                aesAlg.BlockSize = 128;
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(value);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        var str = Convert.ToBase64String(result);
                        var fullCipher = Convert.FromBase64String(str);
                        return Task.FromResult(str);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return Task.FromResult(string.Empty);
        }
    }

    public Task<string> Decrypt(string value, string KeyValue)
    {
        if (string.IsNullOrEmpty(value)) return Task.FromResult(value);
        try
        {
            value = value.Replace(" ", "+");
            var fullCipher = Convert.FromBase64String(value);

            var iv = new byte[16];
            var cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
            var key = Encoding.UTF8.GetBytes(KeyValue);

            using (var aesAlg = Aes.Create())
            {
                aesAlg.BlockSize = 128;
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
        catch (Exception ex)
        {
            return Task.FromResult(string.Empty);
        }
    }


    public Task<string> CreateHash(string unencrypted, string keyString)
    {
        if (string.IsNullOrEmpty(unencrypted)) return Task.FromResult(unencrypted);
        if (string.IsNullOrEmpty(keyString)) return Task.FromResult(keyString);
        try
        {
            string saltyPassword = unencrypted + keyString;
            byte[] saltyByteArray = Encoding.UTF8.GetBytes(saltyPassword);


            byte[] saltyHashByteArray = SHA512.HashData(saltyByteArray);

            return Task.FromResult(Convert.ToBase64String(saltyHashByteArray));
        }
        catch (Exception ex)
        {
            return Task.FromResult(string.Empty);
        }
    }

    public Task<bool> CompareHash(string unencrypted, string keyString, string hashedValue)
    {
        if (string.IsNullOrEmpty(unencrypted)) return Task.FromResult(false);
        if (string.IsNullOrEmpty(keyString)) return Task.FromResult(false);
        try
        {
            string saltyHash = CreateHash(unencrypted, keyString).Result;
            return Task.FromResult(saltyHash.Equals(hashedValue));
        }
        catch (Exception ex)
        {
            return Task.FromResult(false);
        }
    }

    #region old code delete
    public Task<string> OldEncrypt(string unencryptedText, string keyString)
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
    public Task<string> OldEncrypt(string unencryptedText, byte[] keyArray)
    {
        using (var aesAlg = Aes.Create())
        {
            using (var encryptor = aesAlg.CreateEncryptor(keyArray, aesAlg.IV))
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

    public Task<string> OldDecrypt(string encryptedText, string keyString)
    {
        var fullCipher = Convert.FromBase64String(encryptedText);

        var iv = new byte[16];
        //var cipher = new byte[16];
        var cipher = new byte[fullCipher.Length - iv.Length];

        //Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
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

    public Task<string> OldDecrypt(string encryptedText, byte[] keyArray)
    {
        var fullCipher = Convert.FromBase64String(encryptedText);

        var iv = new byte[16];
        var cipher = new byte[16];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);

        using (var aesAlg = Aes.Create())
        {
            using (var decryptor = aesAlg.CreateDecryptor(keyArray, iv))
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
    #endregion
}