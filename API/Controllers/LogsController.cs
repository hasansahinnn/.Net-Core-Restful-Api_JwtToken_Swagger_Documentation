using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeGelio.Service.LoginProcess;
using Newtonsoft.Json;
using Saglikcim.Entities.ServiceUtilities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class LogsController : ControllerBase
    {

        private readonly LoginUser loginUser;
        public LogsController(IHttpContextAccessor httpContext)
        {
            var token = httpContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            JwtDecoder jwtHelper = new JwtDecoder();
            loginUser = jwtHelper.DecodeJwt(token);  // Get User Back
        }

        /// <summary>
        /// Logları Kaydetme
        /// </summary>
        /// <param name="datas"></param>
        /// <remarks>
        /// {page}
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public IActionResult LogsAdd(object datas)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(datas.ToString());             
                LogsServices.Log(data["page"], "", 2);
                return Ok(ResponseHandler.ResponseMessageHandler(200, "Success")); 
            }
            catch (Exception ex)
            {
                LogsServices.Log("Logs - Add", ex.Message.ToString(), 3);
                return Ok(ResponseHandler.ResponseMessageHandler(400, "Error"));
            }
                       
        }
    }
}