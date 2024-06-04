using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using System.Xml.Linq;
using MobAppCoreAPI.Interfaces;
using MobAppCoreAPI.Models.Response;
using FourQT.CommonFunctions;
using FourQT.Masters;
using FourQT.Entities;
using FourQT.Reports;
using FourQT.Utilities;
namespace MobAppCoreAPI.Repository
{
    public class ListalltodaySiteVisitRepository:ItodaysiteVisit
    {
        public async Task<dynamic> ListDashboardTodaySVScheduled(HttpRequest req,int page_index, int page_size)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            generalResponse.IsSuccess = true;
            generalResponse.Status = HttpStatusCode.OK;
            generalResponse.Message = "Success";

            try
            {
                JWTTokenMethods jwt = new JWTTokenMethods();
                //JwtTokenAuthorize jwtauth = new JwtTokenAuthorize();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);
                //jwtauth.GetConnectionDetails(req, out int loginIdd, out string mKeyy,out string SecToken);
                generalResponse.Data = await (new DashboardBLL()).GetDashboardTodaySiteVisitScheduled(mKey, loginId,page_index,page_size);

                return generalResponse;
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "v1/dashboard-sitevisit");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                return generalResponse;
            }
        }
    }
}
