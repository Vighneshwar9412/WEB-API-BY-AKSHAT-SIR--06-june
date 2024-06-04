using BrokerPortalAPI.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using FourQT.CommonFunctions;
using FourQT.Core;
using FourQT.Entities;
using System.Threading.Tasks;
using BrokerPortalAPI.Interfaces;

namespace BrokerPortalAPI.Controllers
{
    [Route("api/v2/inventory-list")]
    [ApiController]
    [APIKey]
    public class InventoryController : ControllerBase
    {
        private readonly  IInventory _inventoryList;

        public InventoryController(IInventory inventoryList)
        {
            _inventoryList = inventoryList;
        }

        [HttpGet]
        public async Task<dynamic> getInventoryList([FromHeader] int projectId, [FromHeader] int towerId, [FromHeader] string type)
        {
            return await _inventoryList.getInventoryList(Request,projectId ,towerId,type);
        }



        //public async Task<dynamic> GetInventoryList([FromHeader] int projectid, [FromHeader] int towerid, [FromHeader] string type)
        //{
        //    JWTTokenMethods jwt = new JWTTokenMethods();
        //    //JwtTokenAuthorize jwtauth = new JwtTokenAuthorize();
        //    jwt.GetConnectionDetails(Request, out int loginId, out string mKey);
        //    return (APIObjectResponse)(new InventoryBLL()).getInventoryList(mKey, loginId, projectid, towerid, type);
        //}
        
    }
}
