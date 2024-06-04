using FourQT.Entities.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Controllers
{   
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class LogoutController : ControllerBase
    {
        private readonly ILogout _Ilogout;

        public LogoutController(ILogout Ilogout)
        {
            _Ilogout = Ilogout;
        }

        [Route("api/v1/logout")]
        [HttpGet]
        public async Task<dynamic> logout()
        {
            return await _Ilogout.logout(Request);
        }

        [Route("/api/v1/ChangePasswordLead")]
        [HttpPost]
        public async Task<dynamic> ChangePassword([FromBody] ChangePasswordLeadRequest model)
        {
            return await _Ilogout.changePassword(model, Request, HttpContext);
        }
    }
}
