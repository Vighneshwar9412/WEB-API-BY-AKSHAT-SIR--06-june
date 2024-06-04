using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/GetTeamWiseEmployees")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class GetTeamWiseEmployeesController : ControllerBase
    {
        private readonly IGetTeamWiseEmployees _IGetTeamWiseEmployees;

        public GetTeamWiseEmployeesController(IGetTeamWiseEmployees IGetTeamWiseEmployees)
        {
            _IGetTeamWiseEmployees = IGetTeamWiseEmployees;
        }

        [HttpGet]
        public async Task<dynamic> GetTeamWiseEmployees()
        {
            return await _IGetTeamWiseEmployees.GetTeamWiseEmployees(Request);
        }
    }
}
