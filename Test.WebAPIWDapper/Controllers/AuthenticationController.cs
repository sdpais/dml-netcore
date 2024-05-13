﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Test.WebAPIWDapper.Services;
using WebAPIWDapper.Models;
using WebAPIWDapper.Services;

namespace WebAPIWDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IRedisCacheHandler _redisCacheHandler;
        public AuthenticationController(IConfiguration config)
        {
            _config = config;
            _redisCacheHandler = new RedisCacheHandler(config);
        }

        [HttpPost("logintest")]
        public IActionResult LoginTest([FromBody] Login ? user)
        {
            if(user is null || user.UserName is null || user.Password is null)
            {
                return BadRequest("Invalid user request");
            }
            if (user.UserName == "sanjay" && user.Password == "pais")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JWT:Secret")));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(issuer: _config.GetValue<string>("JWT:ValidIssuer"), audience: _config.GetValue<string>("JWT:ValidAudience"), claims: new List<Claim>(), expires: DateTime.Now.AddMinutes(6), signingCredentials: signinCredentials);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                _redisCacheHandler.StringSetAsync(user.UserName + ".Token", tokenString, new TimeSpan(0, 0, 60));
                return Ok(new JWTToken
                {
                    Token = tokenString
                });
                
            }
            return Unauthorized();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login? user)
        {
            if (user is null || user.UserName is null || user.Password is null)
            {
                return BadRequest("Invalid user request");
            }
            if(user is not null)
            {
                ILoginService _loginService = new LoginService(new DbService(_config));
                var result = _loginService.GetLogin(user.UserName);
                if (result is not null)
                {
                    // how do i extract the password from the result and compare it with the user.Password


                    //var userFromResult = (Login)result;
                    //if(user.Password == result)
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JWT:Secret")));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(issuer: _config.GetValue<string>("JWT:ValidIssuer"), audience: _config.GetValue<string>("JWT:ValidAudience"), claims: new List<Claim>(), expires: DateTime.Now.AddMinutes(6), signingCredentials: signinCredentials);
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    _redisCacheHandler.StringSetAsync(user.UserName + ".Token", tokenString, new TimeSpan(0, 0, 60));
                    return Ok(new JWTToken
                    {
                        Token = tokenString
                    });
                }
            }
            return Unauthorized();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Login? user)
        {
            if (user is null || user.UserName is null || user.Password is null)
            {
                return BadRequest("Invalid user request");
            }
            if (user is not null)
            {
                ILoginService _loginService = new LoginService(new DbService(_config));
                var result =  await _loginService.CreateLogin(user);
                return Ok(result);
            }

            return BadRequest();

        }
    }
}
