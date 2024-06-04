using FourQT.Entities.Employee;
using MobAppCoreAPI.Interfaces.Employee;
using FourQT.Core.Employee;
using FourQT.Entities.General;

namespace MobAppCoreAPI.Repository.Employee
{
    public class InventoryOperationsRepository : IInventoryOperations
    {
        public async Task<dynamic> holdInventory(HoldUnitRequest model, HttpRequest req,HttpContext context)
        {
            return await (new InventoryOperationsBLL()).holdInventory(model, req, context);
        }

        public async Task<dynamic> unholdInventory(HoldUnitRequestU model,HttpRequest req, HttpContext context)
        {
            return await (new InventoryOperationsBLL()).unholdInventory(model, req, context);
        }

        public async Task<dynamic> sellInventory(SellInventoryRequest model, HttpRequest req,HttpContext context)
        {
            return await (new InventoryOperationsBLL()).sellInventory(model, req, context);
        }

        public async Task<dynamic> listSoldBookedInventoryDetails(InventoryRequestLong model, HttpRequest req,HttpContext context)
        {
            return await (new InventoryOperationsBLL()).listSoldBookedInventoryDetails(model, req, context);
        }

        public async Task<dynamic> getUploadDocumentList(DocumentListRequest model, HttpRequest req,HttpContext context)
        {
            return await (new InventoryOperationsBLL()).getUploadDocumentList(model, req, context);
        }

        public async Task<dynamic> uploadDocuments(UploadDocumentRequest model,HttpRequest req,HttpContext context)
        {
            return await (new InventoryOperationsBLL()).uploadDocuments(model, req, context);
        }

        public async Task<dynamic> FileUploadTestAPI(FileUploadRequest model, HttpRequest req, HttpContext context)
        {
            return await(new InventoryOperationsBLL()).FileUploadTestAPI(model, req, context);
        }
    }
}
