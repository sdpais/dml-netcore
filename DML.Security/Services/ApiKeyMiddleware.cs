using Microsoft.AspNetCore.Http;
using DML.Utilities.Constants;
using System.Net;


/*
Reference:
https://code-maze.com/aspnetcore-api-key-authentication/
https://www.youtube.com/watch?app=desktop&v=CV6VdBR86co&ab_channel=MilanJovanovi%C4%87
https://medium.com/@cprafulm_3258/secure-web-api-by-key-authentication-in-asp-net-core-5b96671d5537
*/
namespace DML.Security.Services
{
    public class ApiKeyMiddleware
    {
        //ApiKeyValidator: IApiKeyValidator
        private readonly RequestDelegate _next;
        private readonly IApiKeyValidator _apiKeyValidator;

        public ApiKeyMiddleware(RequestDelegate next, IApiKeyValidator apiKeyValidator)
        {
            _next = next;
            _apiKeyValidator = apiKeyValidator;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (string.IsNullOrWhiteSpace(context.Request.Headers[APIKeys.ApiKeyHeaderName]))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            string? userApiKey = context.Request.Headers[APIKeys.ApiKeyHeaderName];

            if (!_apiKeyValidator.IsValid(userApiKey!))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            await _next(context);
        }
    }
}
