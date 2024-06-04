using FourQT.CommonFunctions;
using FourQT.Entities;
using FourQT.Reports;
using MobAppCoreAPI.Interfaces;
using System.Net;
using FourQT.Utilities;

namespace MobAppCoreAPI.Repository
{
    public class FollowUpListingRepository:IFollowUplisting
    {
        public async Task<dynamic> followupListing(HttpRequest req,int enq_id)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            generalResponse.IsSuccess = true;
            generalResponse.Status = HttpStatusCode.OK;
            generalResponse.Message = "Success";

            try
            {
                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);
                generalResponse.Data = await (new FollowUpBLL()).followuplisting(mKey, loginId,enq_id);

                return generalResponse;
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/followup-listing");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                return generalResponse;
            }
        }

        public async Task<dynamic> getTodayFollowup_Notification(HttpRequest req)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                (new JWTTokenMethods()).GetConnectionDetails(req, out int loginId, out string mKey);

                generalResponse.Data = await (new FollowUpBLL()).GetTodayFollowup_Notification(mKey, loginId);
                generalResponse.IsSuccess = true;
                generalResponse.Message = "Success";
                generalResponse.Status = HttpStatusCode.OK;
                generalResponse.Title = "Success";
            }
            catch (Exception ex) {
                Utility.LogErrorText(ex.ToString(), "api/v1/GetTodayFollowup_Notification");

                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Status = HttpStatusCode.BadRequest;
                generalResponse.Title = "Error";
            }

            return generalResponse;
        }
    }
}
