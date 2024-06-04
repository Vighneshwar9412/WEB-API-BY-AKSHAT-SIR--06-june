using FourQT.Entities;
using FourQT.Entities.General;
using FourQT.Entities.Portal;

namespace MobAppCoreAPI.Interfaces.General
{
    public interface ICommonLogin
    {        
        public Task<APIObjectResponse> getCommonLogin(CommonLoginRequest login,HttpContext con);
        public Task<dynamic> forgotPassword(ForgotPasswordRequest model,HttpContext context);
    }
}
