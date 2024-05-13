using WebAPIWDapper.Services;

namespace Test.WebAPIWDapper.BusinessLogic
{
    public class EncryptionService
    {
        private readonly IDbService _dbService;
        public EncryptionService(IConfiguration configuration)
        {
            _dbService = new DbService(configuration);
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
