using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using System.Xml.Linq;
using MobAppCoreAPI.Interfaces;
using MobAppCoreAPI.Models.Response;
using FourQT.CommonFunctions;
using FourQT.Masters;
using FourQT.Entities;
using FourQT.Core;
using FourQT.Utilities;
using System;

namespace MobAppCoreAPI.Repository
{
    public class MobAppCallRepository : IMobileAppCall
    {

        public async Task<APIObjectResponse> recordMobAppCall(HttpRequest req, MobAppCall model, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);

                generalResponse = await (new MobAppCallBLL()).RecordMobAppCall(mKey, loginId, model);
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/RecordMobAppCall");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                generalResponse.Title = "Error";
            }

            return generalResponse;
        }

        public async Task<APIObjectResponse> mobAppCallReport(HttpRequest req, MobAppCallReportReq model, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);

                generalResponse = await (new MobAppCallBLL()).mobAppCallReport(mKey, loginId, model);
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/MobAppCallReport");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                generalResponse.Title = "Error";
            }

            return generalResponse;
        }

    }
}
