using FourQT.Entities.Employee;
using FourQT.Entities.Portal;

namespace MobAppCoreAPI.Interfaces.Employee
{
    public interface IInventory
    {
        public Task<dynamic> getProject(HttpRequest req);
        public Task<dynamic> getMastersByProject(InventoryRequestShort model, HttpRequest req, HttpContext context); 
        public Task<dynamic> getInventory(InventoryRequestLong model, HttpRequest req, HttpContext context); 
        public Task<dynamic> getTowerWiseFloor(FloorRequest model, HttpRequest req, HttpContext context);
    }
}
