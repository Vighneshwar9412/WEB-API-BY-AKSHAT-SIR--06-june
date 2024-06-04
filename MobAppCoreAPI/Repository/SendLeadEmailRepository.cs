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
    public class SendLeadEmailRepository:ILeadSendEmail
    {
        public async Task<dynamic> SendleadEmail(HttpRequest req, SendEmail mail,HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(mail);
                Log.LogPayloadDateWise(message, "SendLeadEmail", context);

                JWTTokenMethods jwt = new JWTTokenMethods();
            
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);
            
                generalResponse = await (new SendSMSBLL()).Sendleademail(mKey, loginId, mail);

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
