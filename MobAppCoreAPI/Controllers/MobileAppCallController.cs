using FourQT.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/MobileAppCall/")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class MobileAppCallController : ControllerBase
    {
        public readonly IMobileAppCall _mobAppCall;

        public MobileAppCallController(IMobileAppCall mobAppCall)
        {
            _mobAppCall = mobAppCall;
        }


        [HttpPost]
        [Route("RecordMobAppCall")]
        public async Task<APIObjectResponse> RecordMobAppCall([FromBody] MobAppCall model)
        {
            return await _mobAppCall.recordMobAppCall(Request, model, HttpContext);
        }

        [HttpPost]
        [Route("MobAppCallReport")]
        public async Task<APIObjectResponse> MobAppCallReport([FromBody] MobAppCallReportReq model)
        {
            return await _mobAppCall.mobAppCallReport(Request, model, HttpContext);
        }

    }
}
