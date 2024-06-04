using FourQT.CommonFunctions;
using FourQT.Core;
using FourQT.Entities;
using FourQT.Reports;
using MobAppCoreAPI.Interfaces;
using FourQT.Utilities;
using System.Diagnostics;
using FourQT.CommonFunctions.Portal;
using Newtonsoft.Json;

namespace MobAppCoreAPI.Repository
{
    public class SaveFollowUpRepository:IFollowUpSave
    {
        public async Task<dynamic> followupsave(HttpRequest req, ShortFollowUpSave lee,HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(lee);
                Log.LogPayloadDateWise(message, "FollowupSave", context);

                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);
                generalResponse = (APIObjectResponse)(new FollowUpSaveBLL()).followupSave(mKey, lee,loginId);

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
