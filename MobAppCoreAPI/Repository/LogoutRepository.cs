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
using FourQT.UserRights;
using FourQT.Utilities;
using FourQT.Entities.Employee;
using FourQT.CommonFunctions.Portal;
using Newtonsoft.Json;
namespace MobAppCoreAPI.Repository
{
    public class LogoutRepository : ILogout
    {
        public async Task<dynamic> logout(HttpRequest req)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                JWTTokenMethods jwt = new JWTTokenMethods();
                //JwtTokenAuthorize jwtauth = new JwtTokenAuthorize();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);
                //jwtauth.GetConnectionDetails(req, out int loginIdd, out string mKeyy,out string SecToken);
                generalResponse= (APIObjectResponse)(new UserRightsBLL()).logout(mKey, loginId);
                
                
               
                return generalResponse;
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "v1/logout");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                return generalResponse;
            }
        }

        public async Task<dynamic> changePassword(ChangePasswordLeadRequest model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "ChangePasswordLead", context);

                generalResponse = await (new UserRightsBLL()).changePassword(model, req);
            }
            catch (Exception ex) {
                generalResponse.IsSuccess = false;
                generalResponse.Title = "Error";
                generalResponse.Status = HttpStatusCode.BadRequest;
                generalResponse.Message = ex.Message;
            }

            return generalResponse;
        }
    }
}
