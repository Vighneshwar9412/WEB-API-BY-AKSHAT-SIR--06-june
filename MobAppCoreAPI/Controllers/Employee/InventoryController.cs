using FourQT.Entities.Employee;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes.Employee;
using MobAppCoreAPI.Interfaces.Employee;
using Swashbuckle.AspNetCore.Annotations;

namespace MobAppCoreAPI.Controllers.Employee
{
    [Route("/api/e1/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "e1")]
    [APIEmployeeKey]
    public class InventoryController : ControllerBase
    {
        private readonly IInventory _IInventory;

        public InventoryController(IInventory IInventory)
        {
            _IInventory = IInventory;
        }

        [HttpGet]
        public async Task<dynamic> GetInventoryProjectList()
        {
            return await _IInventory.getProject(Request);
        }

        [HttpPost]
        public async Task<dynamic> GetInventoryMasters([FromBody] InventoryRequestShort model)
        {
            return await _IInventory.getMastersByProject(model, Request, HttpContext);
        }

        [HttpPost]
        [SwaggerOperation(Description = "GetInventoryUnitStatus ( \r\ntype : 'A' Available / 'H' Hold / 'B' Booked /\r\n 'ALL' All) ,\r\nunitNo : '' (Must be empty string to get list),\r\nminArea: 0 ,\r\nmaxArea : 100000 (must be some decimal \r\nvalue to get list between range)")]
        public async Task<dynamic> GetInventoryUnitStatus([FromBody] InventoryRequestLong model)
        {
            return await _IInventory.getInventory(model, Request, HttpContext);
        }

        [HttpPost]
        public async Task<dynamic> GetInventoryTowerWiseFloor([FromBody] FloorRequest model)
        {
            return await _IInventory.getTowerWiseFloor(model, Request, HttpContext);
        }
    }
}
