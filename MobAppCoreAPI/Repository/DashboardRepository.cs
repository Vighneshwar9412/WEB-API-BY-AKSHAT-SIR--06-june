using System.Xml.Linq;
using MobAppCoreAPI.Interfaces;
using MobAppCoreAPI.Models.Response;
using FourQT.CommonFunctions;
using FourQT.Masters;
using FourQT.Entities;
using System.Net;
using FourQT.Reports;
using FourQT.Utilities;

namespace MobAppCoreAPI.Repository
{
    public class DashboardRepository:Idashboard
    {
        public async Task<dynamic> geticonleadsvisit(HttpRequest req)
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
                generalResponse.Data = await (new DashboardBLL()).GetDashboardWrapper(mKey, loginId);

                return generalResponse;
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "v1/dashboard");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                return generalResponse;
            }
        }
    }
}
