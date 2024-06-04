using FourQT.Entities.HR;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes.Portal_P2;
using MobAppCoreAPI.Interfaces.Portal_P2;

namespace MobAppAPI.Controllers
{
    [Route("/api/p2/[controller]/[action]")]
    [ApiController]
    [APIPortalKey]
    [ApiExplorerSettings(GroupName = "p2")]
    public class HRController : ControllerBase
    {
        private readonly IPortalHR _IPortalHR;
        public HRController(IPortalHR IPortalHR)
        {
            _IPortalHR = IPortalHR;
        }

        [HttpPost]
        public List<AttendanceResponseModel> HRAttendanceLogin(AttendanceRequestModel objAttendanceRequest)
        {
            return _IPortalHR.HRAttendanceLogin(objAttendanceRequest,Request);
        }
        
    }
}
