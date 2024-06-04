using FourQT.Entities.Employee;
using FourQT.Entities.General;
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
    public class InventoryOperationsController : ControllerBase
    {
        private readonly IInventoryOperations _IInventoryOperations;

        public InventoryOperationsController(IInventoryOperations IInventoryOperations)
        {
            _IInventoryOperations = IInventoryOperations;
        }

        [HttpPost]
        public async Task<dynamic> HoldUnit(HoldUnitRequest model)
        {
            return await _IInventoryOperations.holdInventory(model,Request,HttpContext);
        }

        [HttpPost]
        public async Task<dynamic> UnholdUnit(HoldUnitRequestU model)
        {
            return await _IInventoryOperations.unholdInventory(model,Request,HttpContext);
        }

        [HttpPost]
        public async Task<dynamic> SellUnit(SellInventoryRequest model)
        {
            return await _IInventoryOperations.sellInventory(model,Request,HttpContext);
        }

        [HttpPost]
        [SwaggerOperation(Description = " ( type : 'A' All/ 'S' Sold/ 'B' Booked ) ,\r\nunitNo : '' (Must be empty string to get list),\r\nminArea: 0 ,\r\nmaxArea : 100000 (must be some decimal \r\nvalue to get list between range)")]
        public async Task<dynamic> ListSoldBookedInventoryDetails(InventoryRequestLong model)
        {
            return await _IInventoryOperations.listSoldBookedInventoryDetails(model,Request,HttpContext);
        }

        [HttpPost]
        public async Task<dynamic> GetUploadDocumentList(DocumentListRequest model)
        {
            return await _IInventoryOperations.getUploadDocumentList(model,Request,HttpContext);
        }

        [HttpPost]
        [SwaggerOperation(Description = "image: Base64 string on document, action: 'I' Upload/Insert, 'D' Delete")]
        public async Task<dynamic> UploadDocuments(UploadDocumentRequest model)
        {
            return await _IInventoryOperations.uploadDocuments(model,Request,HttpContext);
        }

        [HttpPost]
        [SwaggerOperation(Description ="***** TEST API FOR FILE UPLOAD *****")]
        public async Task<dynamic> FileUploadTestAPI(FileUploadRequest model)
        {
            return await _IInventoryOperations.FileUploadTestAPI(model, Request, HttpContext);
        }
    }
}
