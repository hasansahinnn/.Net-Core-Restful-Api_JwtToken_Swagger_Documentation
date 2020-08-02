using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Data.Services;
using Data.Domain;
using Saglikcim.Entities.ServiceUtilities;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class CountryController : ControllerBase
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Get()
        {
            try
            {
                    CountryServices country = new CountryServices();
                    return Ok(ResponseHandler.ResponseMessageHandler(200, "Success", country.ToList()));
            }
            catch (Exception ex)
            {
                //LogsServices.Log("Country - Get", ex.Message.ToString(), 3);
                return Ok(ResponseHandler.ResponseMessageHandler(400, "Error"));
            }
            
        }
    }
}