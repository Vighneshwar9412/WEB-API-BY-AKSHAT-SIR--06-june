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
using FourQT.Core;
using FourQT.Utilities;
using FourQT.CommonFunctions.Portal;
using Newtonsoft.Json;

namespace MobAppCoreAPI.Repository
{
    public class LeadSuccessRepository :ILeadSuccess
    {
        public async Task<dynamic> leadsuccess(HttpRequest req, LeadSuccess lee, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(lee);
                Log.LogPayloadDateWise(message, "SuccessLead", context);

                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);
                generalResponse = (APIObjectResponse)(new DumpLeadBLL()).leadsuccess(mKey, lee, loginId);

                return generalResponse;
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/success-lead");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                return generalResponse;
            }
        }
    }
}
