using FourQT.CommonFunctions;
using FourQT.Core;
using FourQT.Entities;
using MobAppCoreAPI.Interfaces;
using FourQT.Utilities;
using FourQT.Notifications;
using FourQT.CommonFunctions.Portal;
using Newtonsoft.Json;

namespace MobAppCoreAPI.Repository
{
    public class SendLeadSMSRepository :ISendleadSMS 
    {
        public async Task<dynamic> SendleadSMS(HttpRequest req,SendSMS sms,HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            //generalResponse.IsSuccess = true;
            //generalResponse.Status = HttpStatusCode.OK;
            //generalResponse.Message = "Success";

            try
            {
                string message = JsonConvert.SerializeObject(sms);
                Log.LogPayloadDateWise(message, "SendLeadSMS", context);

                JWTTokenMethods jwt = new JWTTokenMethods();
                //JwtTokenAuthorize jwtauth = new JwtTokenAuthorize();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);
                //jwtauth.GetConnectionDetails(req, out int loginIdd, out string mKeyy,out string SecToken);
                generalResponse = await (new SendSMSBLL()).Sendleadsms(mKey,loginId, sms);

                return generalResponse;
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/lead-icon");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                return generalResponse;
            }
        }
    }
}
