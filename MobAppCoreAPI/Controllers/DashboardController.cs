using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Controllers
{

    [Route("api/v1/dashboard")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class DashboardController : ControllerBase
    {
        private readonly Idashboard _IDashboard;
        public DashboardController(Idashboard Idashboard)
        {
            _IDashboard= Idashboard;
        }

        [HttpGet]
        
        public async Task<dynamic> geticonleadsvisit()
        {
            return await _IDashboard.geticonleadsvisit(Request);
            //MemoryCache
        }
    }
}
