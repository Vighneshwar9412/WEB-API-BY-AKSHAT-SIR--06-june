using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;


namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/dashboard-sitevisit")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class TodaySiteVisitController : ControllerBase
    {

        private readonly ItodaysiteVisit _ITodaySiteVisit;
        public TodaySiteVisitController(ItodaysiteVisit ItodaysiteVisit)
        {
            _ITodaySiteVisit = ItodaysiteVisit;
        }

        [HttpGet]
        public async Task<dynamic> listtodaysiteVisit(int page_index,int page_size)
        {
            return await _ITodaySiteVisit.ListDashboardTodaySVScheduled(Request,page_index,page_size);
        }
    }
}

