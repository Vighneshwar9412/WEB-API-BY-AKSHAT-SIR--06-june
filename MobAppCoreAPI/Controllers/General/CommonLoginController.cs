using FourQT.Entities;
using FourQT.Entities.General;
using FourQT.Entities.Portal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobAppCoreAPI.Interfaces.General;
using MobAppCoreAPI.Interfaces.Portal;
using Swashbuckle.AspNetCore.Annotations;

namespace MobAppCoreAPI.Controllers.General
{
    [Route("/api/g1/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "g1")]
    public class CommonLoginController : ControllerBase
    {
        private readonly ICommonLogin _ICommonLogin;

        public CommonLoginController(ICommonLogin ICommonLogin)
        {
            _ICommonLogin = ICommonLogin;
        }      

        [HttpPost]
        [SwaggerOperation(Description = "Common login \r\n ( Token : eKey from ValidateKey,\r\n   DeviceId : device fcm token,\r\n   fromDevice : 1 (Android) / 2 (IOS) \r\n)\r\n(type: 'C' Customer Portal / 'E' Employee / 'CP' Channel Partner)")]
        public async Task<ActionResult<APIObjectResponse>> GetCommonLogin(CommonLoginRequest login)
        {
            return await _ICommonLogin.getCommonLogin(login,HttpContext);
        }

        [HttpPost]
        [SwaggerOperation(Description = "ForgotPassword\r\n( token : eKey from ValidateKey)\r\n(commonLoginType: 'C' Customer Portal,\r\n 'CP' Channel Partner)\r\n\r\n* NOT VALID FOR Employee")]
        public async Task<dynamic> ForgotPassword([FromBody] ForgotPasswordRequest model)
        {
            return await _ICommonLogin.forgotPassword(model,HttpContext);
        }

    }
}
