namespace DML.Security.Services;
public interface IApiKeyValidator
{
    public bool IsValid(string apiKey);
}
