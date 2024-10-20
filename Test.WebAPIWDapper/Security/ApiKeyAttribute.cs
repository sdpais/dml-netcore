using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

/*
Reference:
https://code-maze.com/aspnetcore-api-key-authentication/
https://www.youtube.com/watch?app=desktop&v=CV6VdBR86co&ab_channel=MilanJovanovi%C4%87
https://medium.com/@cprafulm_3258/secure-web-api-by-key-authentication-in-asp-net-core-5b96671d5537
*/
namespace WebAPIWDapper.Security
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute() : base(typeof(ApiKeyAuthFilter)) {
        }

    }

}
