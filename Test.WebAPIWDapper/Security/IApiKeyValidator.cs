namespace WebAPIWDapper.Security
{
    public interface IApiKeyValidator
    {
        public bool IsValid(string apiKey);
    }
}
