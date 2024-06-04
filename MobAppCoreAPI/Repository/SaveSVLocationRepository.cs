using FourQT.CommonFunctions;
using FourQT.Core;
using FourQT.Entities;
using MobAppCoreAPI.Interfaces;
using FourQT.Utilities;
using FourQT.CommonFunctions.Portal;
using Newtonsoft.Json;

namespace MobAppCoreAPI.Repository
{
    public class SaveSVLocationRepository:ISaveSVLocation
    {
        public async Task<dynamic> SVlocationsave(HttpRequest req, SVLocationShort lee, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(lee);
                Log.LogPayloadDateWise(message, "SaveSVLocation", context);

                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);
                generalResponse.Data = (APIObjectResponse)(new SaveSVLocationBLL()).saveSVlocation(mKey, lee, loginId);

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
