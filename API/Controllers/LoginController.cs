using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Data.Domain;
using Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Saglikcim.Entities.ServiceUtilities;

namespace OnlineSaglikcimJWTDeneme.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="datas"></param>
        /// /// <remarks>
        /// {user.Email,user.Password}
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(object datas) // sen new{Email,Password}
        {
            try
            {
            var logindata = JsonConvert.DeserializeObject<IDictionary<string,string>>(datas.ToString());
            IActionResult response = Unauthorized();
            User data = new Data.Services.UserServices().login(logindata.First().Value, logindata.Last().Value);
            if (data!=null)
            {
                var StrToken = GenerateJSONWebToken(data.Id,data.FullName(),data.Email,data.RoleGroup.Name);

                    // If you want to decode jwt, Look LogsController
                response = Ok(ResponseHandler.ResponseMessageHandler(200, "Success", new { title = "Success", token = StrToken }));
            }
            return response;
            }
            catch (Exception ex)
            {
                return Ok(ResponseHandler.ResponseMessageHandler(400, "Error"));
            }
            
        }
        //
        [ApiExplorerSettings(IgnoreApi = true)]//swagger tarafından görüllmesini istemidiğimiz fonksiyonlara yazılmalıdır.
        private string GenerateJSONWebToken(int id,string name,string email,string rolename)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                  {
                    new Claim(ClaimTypes.Actor, id.ToString()),
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, rolename),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                    );
                var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
                return encodetoken;
            }
            catch (Exception ex)
            {
                //LogsServices.Log("Login - GenerateJSONWebToken", ex.Message.ToString(), 3);
                return null;
            }
           
        }
     
    
    }
}