using FourQT.Entities;
using FourQT.Entities.Employee;

namespace MobAppCoreAPI.Interfaces
{
    public interface ILeadInventory
    {
        public Task<APIObjectResponse> getInventoryMasters(InventoryRequestMasters model, HttpRequest req, HttpContext context);
        public Task<APIObjectResponse> getInventoryUnitStatus(InventoryRequestLong model, HttpRequest req, HttpContext context);         
    }
}
