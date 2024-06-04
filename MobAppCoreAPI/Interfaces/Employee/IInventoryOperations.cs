using FourQT.Entities.Employee;
using FourQT.Entities.General;

namespace MobAppCoreAPI.Interfaces.Employee
{
    public interface IInventoryOperations
    {
        public Task<dynamic> holdInventory(HoldUnitRequest model,HttpRequest req, HttpContext context);
        public Task<dynamic> unholdInventory(HoldUnitRequestU model, HttpRequest req, HttpContext context);
        public Task<dynamic> sellInventory(SellInventoryRequest model, HttpRequest req,HttpContext context);       
        public Task<dynamic> listSoldBookedInventoryDetails(InventoryRequestLong model,HttpRequest req, HttpContext context);
        public Task<dynamic> getUploadDocumentList(DocumentListRequest model, HttpRequest req,HttpContext context); 
        public Task<dynamic> uploadDocuments(UploadDocumentRequest model,HttpRequest req,HttpContext context);
        public Task<dynamic> FileUploadTestAPI(FileUploadRequest model, HttpRequest req, HttpContext context);
    }
}
