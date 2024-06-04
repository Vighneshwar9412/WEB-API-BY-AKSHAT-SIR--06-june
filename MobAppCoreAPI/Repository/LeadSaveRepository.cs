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
    public class LeadSaveRepository:ILeadSave
    {
        public async Task<dynamic> leadsave(HttpRequest req, LeadShort lee,HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(lee);
                Log.LogPayloadDateWise(message, "SaveEnquiry", context);

                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);
                return await (new SaveLeadBLL()).leadsave(mKey, lee,loginId);

            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/save-enquiry");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                return generalResponse;
            }
        }

    }
}
