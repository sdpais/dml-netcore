using WebAPIWDapper.Services;

namespace WebAPIWDapper.BusinessLogic
{
    public class EncryptionBLService : BusinessLogicBLBase
    {
        
        public EncryptionBLService(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<bool> LoadAndCacheEncryptionKeys()
        {
            // get all active keys
            EncryptionKeysService encryptionKeysService = new EncryptionKeysService(_dbService);
            var keys = await encryptionKeysService.GetAllActiveKeys();
            //save them into the RedisCache

            //return true if successful
            return true;
        }
            
    }
}
