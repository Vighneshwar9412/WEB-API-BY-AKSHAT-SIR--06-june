using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using FourQT.Entities;
using FourQT.UserRights;
using BrokerPortalAPI.Attributes;
using System.Threading.Tasks;

namespace BrokerPortalAPI.Controllers
{
    [Route("api/v2/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<APIObjectResponse> Login([FromBody] LoginRequestDTO model, [FromHeader] string ekey)
        {
            // return await _loginInterface.LoginAsync(model, mkey);
            return await (new UserRightsBLL()).LoginBrokerAsync(model, ekey);
        }
    }
}
