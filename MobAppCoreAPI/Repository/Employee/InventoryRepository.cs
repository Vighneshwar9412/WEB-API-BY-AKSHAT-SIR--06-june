using FourQT.Entities.Employee;
using FourQT.Masters;
using FourQT.UserRights;
using MobAppCoreAPI.Interfaces.Employee;

namespace MobAppCoreAPI.Repository.Employee
{
    public class InventoryRepository : IInventory
    {
        public async Task<dynamic> getProject(HttpRequest req)
        {
            return await (new InventoryMastersBLL()).getProject(req);
        }

        public async Task<dynamic> getMastersByProject(InventoryRequestShort model, HttpRequest req, HttpContext context)
        {
            return await (new InventoryMastersBLL()).getMastersByProject(model,req, context);
        }

        public async Task<dynamic> getInventory(InventoryRequestLong model, HttpRequest req, HttpContext context)
        {
            return await (new InventoryMastersBLL()).getInventory(model, req, context);
        }

        public async Task<dynamic> getTowerWiseFloor(FloorRequest model, HttpRequest req, HttpContext context)
        {
            return await (new InventoryMastersBLL()).getTowerWiseFloor(model, req, context);
        }
    }
}
