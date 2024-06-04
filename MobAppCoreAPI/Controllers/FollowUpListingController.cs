using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Controllers
{   
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class FollowUpListingController : ControllerBase
    {
        private readonly IFollowUplisting _IFollowUpList;

        public FollowUpListingController(IFollowUplisting IFollowupListing)
        {
            _IFollowUpList = IFollowupListing;
        }

        [HttpGet]
        [Route("api/v1/followup-listing")]
        public async Task<dynamic> followuplisting(int enquiryId)
        {
            return await _IFollowUpList.followupListing(Request, enquiryId);
        }

        [HttpGet]
        [Route("api/v1/GetTodayFollowup_Notification")]
        public async Task<dynamic> GetTodayFollowup_Notification()
        {
            return await _IFollowUpList.getTodayFollowup_Notification(Request);
        }
    }
}
