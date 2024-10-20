using DML.Utilities.Constants;


namespace DML.Security.Services;

public class ApiKeyValidator: IApiKeyValidator
{
    
    public bool IsValid(string apiKey)
    {
        //TODO : Get API Key from WebConfig
        var validApiKey = "DAQWDZASDWWETRVCdscsdfewwerwerW2343453453%^&&@$#%^cweAwdW$5^7u8343453#$#%#$5";//_configuration.GetValue<string>(APIKeys.ApiKeyConfigurationName);
        //TODo: migrade to DB based API Keys with validity
        return apiKey == validApiKey;
    }
}
