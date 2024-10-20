using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using DML.Utilities.Constants;

namespace DML.Security.Services
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly IApiKeyValidator _apiKeyValidation;
        public ApiKeyAuthFilter(IApiKeyValidator apiKeyValidation)
        {
            _apiKeyValidation = apiKeyValidation;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string userApiKey = context.HttpContext.Request.Headers[APIKeys.ApiKeyHeaderName].ToString();
            if (string.IsNullOrWhiteSpace(userApiKey))
            {
                context.Result = new BadRequestResult();
                return;
            }
            if (!_apiKeyValidation.IsValid(userApiKey))
                context.Result = new UnauthorizedResult();
        }
    }
}
