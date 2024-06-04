using FourQT.CommonFunctions.Portal;
using FourQT.Entities.Portal;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes.Portal_P2;
using MobAppCoreAPI.Interfaces.Portal_P2;
using Swashbuckle.AspNetCore.Annotations;

namespace MobAppCoreAPI.Controllers.Portal_P2
{
    [Route("/api/p2/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "p2")]
    public class LoginController : ControllerBase
    {
        private readonly IPortalLogin_P2 _IPortalLogin;

        public LoginController(IPortalLogin_P2 IPortalLogin)
        {
            _IPortalLogin = IPortalLogin;
        }      

        [HttpPost]
        [SwaggerOperation(Description = "Common login \r\n( Token : eKey from ValidateKey,\r\n   DeviceId : device fcm token,\r\n   fromDevice : 1 (Android) / 2 (IOS)\r\n)\r")]
        public async Task<ResponseStatusToken_New<Customeruserlist>> CustomerLogin(CustomerLoginShort objuser)
        {
            return await _IPortalLogin.customerLogin(objuser);
        }

        [HttpPost]
        [APIPortalKey]
        public async Task<ResponseStatus_New<ChangePasswordList>> CustomerChangePassword(ChangePasswordNew objuser)
        {
            return await _IPortalLogin.customerChangePassword(objuser,Request);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<NoData>> GetOTP(CustomerLogin objuser)
        {
            return await _IPortalLogin.getOTP(objuser,Request);
        }

        [HttpPost]
        [APIPortalKey]
        public async Task<ResponseStatus_New<InsertqueryStatus>> CustomerLogOut(CustomerCore1 objuser)
        {
            return await _IPortalLogin.CustomerLogOut(objuser, Request); 
        }
    }
}
