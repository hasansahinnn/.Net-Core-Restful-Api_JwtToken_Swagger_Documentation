using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Domain;
using Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Saglikcim.Entities.ServiceUtilities;

namespace API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class CityController : ControllerBase
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="datas"></param>
        /// <remarks>
        /// {CountryId}
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Get(object datas)
        {
            try
            {
            CityServices cityDb = new CityServices();
            var CountryId = JsonConvert.DeserializeObject<Dictionary<string,int>>(datas.ToString());
            var citys = cityDb.Where(x => x.CountryId == CountryId["CountryId"]).OrderBy(x => x.Name).Select(x => new { id = x.Id, Name = x.Name }).ToList();
            return Ok(ResponseHandler.ResponseMessageHandler(200, "Success", citys));
            }
            catch (Exception ex)
            {
                //LogsServices.Log("City - Get", ex.Message.ToString(), 3);
                return Ok(ResponseHandler.ResponseMessageHandler(400, "Error"));
            }
            
        }
    }
}