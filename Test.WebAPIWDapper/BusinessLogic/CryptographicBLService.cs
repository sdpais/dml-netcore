using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Runtime.InteropServices;
using WebAPIWDapper.Services;
using WebAPIWDapper.Constants;
using WebAPIWDapper.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Infrastructure;
namespace WebAPIWDapper.BusinessLogic
{
    public class CryptographicBLService : BusinessLogicBLBase
    {
        private readonly IRedisCacheHandler _redisCacheHandler;
        public CryptographicBLService(IConfiguration configuration) : base(configuration)
        {
            _redisCacheHandler = new RedisCacheHandler(configuration);
        }
        public async Task<bool> LoadAndCacheEncryptionKeys()
        {
            // get all active keys
            EncryptionKeysService encryptionKeysService = new EncryptionKeysService(_dbService);
            var keys = await encryptionKeysService.GetAllActiveKeys();
            if (keys.Count == 0)
            {
                await RandomlyGenerate5Keys();
                keys = await encryptionKeysService.GetAllActiveKeys();
            }
            //save them into the RedisCache
            Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
            int index = 0;
            string cachekey;
            foreach(EncryptionKey key in keys)
            {
                //TODO: fix the timespan, clean up how the key is defined
                index++;
                cachekey = RedisCacheKeys.EncryptionKeys + "-" + key.Id;
                keyValuePairs.Add(index, cachekey);
                await _redisCacheHandler.StringSetAsync(cachekey, JsonConvert.SerializeObject(key), new TimeSpan(0,5,5));
            }
            //save the kist of encryptionkeys in the cache
            await _redisCacheHandler.StringSetAsync(RedisCacheKeys.EncryptionKeyList, Newtonsoft.Json.JsonConvert.SerializeObject(keyValuePairs), new TimeSpan(0, 5, 5));
            //wonder if this should be a scheduled task
            //check if any keys are expiring in the next x days and generate new key to replace
            //return true if successful
            return true;
        }
        public async Task<string> GetRandomEncryptionKeyValueFromCache()
        {
            //get the list of keys from the cache
            string keyList = await _redisCacheHandler.StringGetAsync(RedisCacheKeys.EncryptionKeyList);
            if (keyList is null)
            {
                await LoadAndCacheEncryptionKeys();
                keyList = await _redisCacheHandler.StringGetAsync(RedisCacheKeys.EncryptionKeyList);
            }
            Dictionary<int, string> keyValuePairs = JsonConvert.DeserializeObject<Dictionary<int, string>>(keyList);
            //get a random key from the list
            int randomkey = GetRandomNumber();
            string cachekey = keyValuePairs[randomkey];
            string key = await _redisCacheHandler.StringGetAsync(cachekey);
            //return the key
            return key;
        }
        public async Task<string>? GetEncryptionKeyValueFromCache(int encryptionKey)
        {
            string cachekey = RedisCacheKeys.EncryptionKeys + "-" + encryptionKey;
            string key = await _redisCacheHandler.StringGetAsync(cachekey);
            //return the key
            return key;
        }
        #region Private Helpers
        private async Task<bool> RandomlyGenerate5Keys()
        {
            for (int i = 0; i < 10; i++)
            {
                await RandomlyGenerateAndInsertKey();
            }
            return true;
        }
        private async Task<bool> RandomlyGenerateAndInsertKey()
        {
            //TODO: Improve encryption keys using https://learn.microsoft.com/en-us/dotnet/standard/security/generating-keys-for-encryption-and-decryption
            EncryptionKeysService encryptionKeysService = new EncryptionKeysService(_dbService);
            EncryptionKey encryptionKey = new EncryptionKey();
            encryptionKey.KeyString = GenerateKey();
            encryptionKey.ExpiryDate = DateTime.Now.AddDays(GetRandomNumber());
            await encryptionKeysService.AddKey(encryptionKey);
            return true;
        }
        private int GetRandomNumber()
        {
            var random = new Random();
            int resultValue = 0;
            while (resultValue == 0 || resultValue == 10)
            {
                resultValue = random.Next(10);
            }
            return resultValue;
        }
        private string GenerateKey()
        {
            string key = "";
            int keyLength = 32;
            while (key.Length < keyLength)
            {
                key += Guid.NewGuid().ToString().Replace("-", "");
                Console.WriteLine($"Key Length: {key.Length}");
            }
            if (key.Length > keyLength)
            {
                key = key.Substring(0, keyLength);
            }
            return key;
        }
        #endregion
    }
}
