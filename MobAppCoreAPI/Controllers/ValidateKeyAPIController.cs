using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MobAppCoreAPI.Interfaces;
using MobAppCoreAPI.Models.Response;
using System.Collections.Generic;
using FourQT.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/validatekey")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ValidateKeyAPIController : ControllerBase
    {
        private readonly IValidateKey _appInterface;

        public ValidateKeyAPIController(IValidateKey appInterface)
        {
            _appInterface = appInterface;
        }

        [HttpGet]       
        public async Task<ActionResult<APIObjectResponse>> ValidateKey(string mKey)
        {

            return await _appInterface.ValidateKey(mKey);

        }
    }
}
