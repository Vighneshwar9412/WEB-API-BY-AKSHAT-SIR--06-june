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
    [Route("api/v1/pagewise-leads")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class LeadsPageWiseController : ControllerBase
    {
        private readonly IListAllLeads _IlistallLeads;
        public LeadsPageWiseController(IListAllLeads listallLeads)
        {
            _IlistallLeads = listallLeads;
        }



        [HttpPost]
        
        public async Task<APIObjectResponse> LeadslistAll([FromBody]LeadFilters model)
        {
            return await _IlistallLeads.LeadslistAll(Request, model,HttpContext);
        }
    }
}
