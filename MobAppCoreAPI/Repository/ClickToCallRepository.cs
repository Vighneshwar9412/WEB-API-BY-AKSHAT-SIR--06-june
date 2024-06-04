using FourQT.CommonFunctions;
using FourQT.Core;
using FourQT.Entities;
using FourQT.Utilities;
using MobAppCoreAPI.Interfaces;
using Newtonsoft.Json;
using FourQT.CommonFunctions.Portal;

namespace MobAppCoreAPI.Repository
{
    public class ClickToCallRepository : IClickToCall
    {
        public async Task<dynamic> clicktocall(HttpRequest req, ClickCall call, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            //generalResponse.IsSuccess = true;
            //generalResponse.Status = HttpStatusCode.OK;
            //generalResponse.Message = "Success";

            try
            {
                string message = JsonConvert.SerializeObject(call);
                Log.LogPayloadDateWise(message, "ClickToCall", context);

                JWTTokenMethods jwt = new JWTTokenMethods();
                //JwtTokenAuthorize jwtauth = new JwtTokenAuthorize();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);
                //jwtauth.GetConnectionDetails(req, out int loginIdd, out string mKeyy,out string SecToken);
                generalResponse = (APIObjectResponse)(new ClickToCallBLL()).TriggerCall(mKey,loginId, call);

                return generalResponse;
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/dump-lead");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                return generalResponse;
            }
        }
    }
}
