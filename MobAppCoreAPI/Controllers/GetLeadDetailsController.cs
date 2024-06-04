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


namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/GetLeadDetails")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class GetLeadDetailsController : ControllerBase
    {
        private readonly IGetLeadDetails _IGetLeadDetails;
        public GetLeadDetailsController(IGetLeadDetails IGetLeadDetails)
        {
            _IGetLeadDetails = IGetLeadDetails;
        }

        [HttpGet]        
        public async Task<APIObjectResponse> GetLeadDetails(int enquiryId)
        {
            return await _IGetLeadDetails.GetLeadDetails(Request, enquiryId);
        }
    }
}
