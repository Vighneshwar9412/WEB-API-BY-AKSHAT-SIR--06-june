using FourQT.Entities;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Controllers
{
    
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class TransferProcessController : ControllerBase
    {
        private readonly ITransferProcess _Itransferprocess;

        public TransferProcessController(ITransferProcess Itransferprocess)
        {
            _Itransferprocess = Itransferprocess;
        }
       
        [Route("api/v1/transfer-process")]
        [HttpPost]
        public async Task<APIObjectResponse> TransferProcess([FromBody] Transfer model)
        {
            return await _Itransferprocess.transferprocess(Request, model,HttpContext);

        }
        [Route("api/v1/bulktransfer-process")]
        [HttpPost]
        public async Task<APIObjectResponse> BulkTransferProcess([FromBody] Transfer model)
        {
            return await _Itransferprocess.bulktransferprocess(Request, model, HttpContext);

        }
    }
}
