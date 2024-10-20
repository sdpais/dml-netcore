using DML.Utilities.Constants;
namespace WebAPIWDapper.Security
{
    public class ApiKeyValidator: IApiKeyValidator
    {
        private readonly IConfiguration _configuration;
        

        public ApiKeyValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValid(string apiKey)
        {
            var validApiKey = _configuration.GetValue<string>(APIKeys.ApiKeyConfigurationName);
            return apiKey == validApiKey;
        }
    }
}
