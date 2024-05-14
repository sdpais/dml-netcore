using Microsoft.Extensions.Configuration;
using WebAPIWDapper.Services;

namespace WebAPIWDapper.BusinessLogic
{
    public class BusinessLogicBLBase
    {
        protected IDbService _dbService;
        protected IConfiguration _configuration;
        public BusinessLogicBLBase(IConfiguration configuration)
        {
            _dbService = new DbService(configuration);
            _configuration = configuration;
        }
    }
}
