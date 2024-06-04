using FourQT.Entities;
using FourQT.Entities.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/LeadInventory/[action]")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class LeadInventoryController : ControllerBase
    {
        public readonly ILeadInventory _leadInventory;

        public LeadInventoryController(ILeadInventory leadInventory)
        {
            _leadInventory = leadInventory;
        }

        [HttpPost]
        public async Task<APIObjectResponse> GetInventoryMasters([FromBody] InventoryRequestMasters model)
        {
            return await _leadInventory.getInventoryMasters(model, Request, HttpContext);
        }

        [HttpPost]
        [SwaggerOperation(Description = "GetInventoryUnitStatus ( \r\ntype : 'A' Available / 'H' Hold / 'B' Booked /\r\n 'ALL' All) ,\r\nunitNo : '' (Must be empty string to get list),\r\nminArea: 0 ,\r\nmaxArea : 100000 (must be some decimal \r\nvalue to get list between range)")]
        public async Task<APIObjectResponse> GetInventoryUnitStatus([FromBody] InventoryRequestLong model)
        {
            return await _leadInventory.getInventoryUnitStatus(model, Request, HttpContext);
        }
    }
}
