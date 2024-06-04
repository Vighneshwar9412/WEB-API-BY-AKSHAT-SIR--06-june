using FourQT.Entities.Construction;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using MobAppCoreAPI.Attributes;

namespace MobAppCoreAPI.Controllers
{
    [Route("/api/con1/[controller]/[action]")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "con1")]
    public class HRController : ControllerBase
    {
        private readonly IHR _iHR;

        public HRController(IHR iHR)
        {
            _iHR = iHR;
        }

        [HttpGet]
        public async Task<dynamic> GetAttendenceReportMonths()
        {
            return await _iHR.getAttendenceReportMonths(Request, HttpContext, "L");
        }

        [HttpPost]
        public async Task<dynamic> GetAttendenceDetails(HRAttendenceReportRequestCore model)
        {
            return await _iHR.getAttendenceDetails(model,Request, HttpContext,"L");
        }

        [HttpPost]
        [SwaggerOperation(Description = "Post Attendence \r\n ( loginAction : 'in' Login / 'out' Logout)")]
        public async Task<dynamic> PostAttendence(HRAttendenceRequest model)
        {
            return await _iHR.postAttendence(model,Request, HttpContext,"L");
        }

        [HttpPost]
        public async Task<dynamic> GetAttendenceReport(HRAttendenceReportRequest model)
        {
            return await _iHR.getAttendenceReport(model,Request, HttpContext,"L");
        }
    }
}
