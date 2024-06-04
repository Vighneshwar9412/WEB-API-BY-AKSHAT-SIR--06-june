using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MobAppCoreAPI.Interfaces;
using MobAppCoreAPI.Attributes;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/dashboard-leads")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class Dashboard_GetTodayLeadsAPIController : ControllerBase
    {
        private readonly IDashboard_GetTodayLeads _IDashboard_GetTodayLeads;
        public Dashboard_GetTodayLeadsAPIController(IDashboard_GetTodayLeads IDashboard_GetTodayLeads)
        {
            _IDashboard_GetTodayLeads = IDashboard_GetTodayLeads;
        }

        [HttpGet]
        [APIKey]    
        public async Task<dynamic> listAllTodayLeads(int page_index,int page_size)
        {
            return await _IDashboard_GetTodayLeads.getTodayLeads(Request,page_index,page_size);
            
        }
     
    }
}
