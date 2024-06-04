using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Interfaces;
using MobAppCoreAPI.Models.Request;
using MobAppCoreAPI.Models.Response;
using FourQT.Entities;
using FourQT.UserRights;
using FourQT.Entities.Employee;

namespace MobAppCoreAPI.Controllers
{   
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]   
    public class LoginAPIController : ControllerBase
    {
        private readonly ILogin _loginInterface;

        public LoginAPIController(ILogin loginInterface)
        {
            _loginInterface = loginInterface;
        }

        [Route("/api/v1/login")]
        [HttpPost]
        public async Task<dynamic> Login([FromBody] LoginRequestDTO model, [FromHeader] string ekey)
        {
            // return await _loginInterface.LoginAsync(model, mkey);
            return await  (new UserRightsBLL()).LoginAsync(model, ekey,HttpContext); 
        }
    }
}
