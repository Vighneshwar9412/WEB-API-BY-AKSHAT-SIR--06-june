using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MobAppCoreAPI.Interfaces;
using MobAppCoreAPI.Attributes;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/sitevisit-listing")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class SVDoneListController : ControllerBase
    {
        private readonly ISVDoneList _ISVSiteVisit;

        public SVDoneListController(ISVDoneList ISVSiteVisit)
        {
            _ISVSiteVisit = ISVSiteVisit;
        }

        [HttpGet]
        public async Task<dynamic> listsvsiteVisit(int enquiryId)
        {
            return await _ISVSiteVisit.listsvsiteVisit(Request, enquiryId);
        }

    }
}
