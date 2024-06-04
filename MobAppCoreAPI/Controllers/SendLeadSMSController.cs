using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MobAppCoreAPI.Interfaces;
using MobAppCoreAPI.Attributes;
using FourQT.Entities;
using FourQT.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net;
using System.Text;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/send-lead-SMS")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class SendLeadSMSController : ControllerBase
    {
        private readonly ISendleadSMS _IsendSMS;

        public SendLeadSMSController(ISendleadSMS sendleadSMS)
        {

            _IsendSMS = sendleadSMS;

        }

        [HttpPost]
        public async Task<dynamic> sendleadSMS([FromBody] SendSMS sms)
        {
            return await _IsendSMS.SendleadSMS(Request,sms,HttpContext);
        }

      
    }
}
