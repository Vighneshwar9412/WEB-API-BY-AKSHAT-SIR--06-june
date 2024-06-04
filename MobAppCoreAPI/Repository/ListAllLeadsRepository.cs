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
using Newtonsoft.Json;
using FourQT.CommonFunctions.Portal;

namespace MobAppCoreAPI.Repository
{
    public class ListAllLeadsRepository:IListAllLeads
    {
        public async Task<dynamic> LeadslistAll(HttpRequest req, LeadFilters lee, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            generalResponse.IsSuccess = true;
            generalResponse.Status = HttpStatusCode.OK;
            generalResponse.Message = "Success";

            try
            {
                string message = JsonConvert.SerializeObject(lee);
                Log.LogPayloadDateWise(message, "LeadListAll", context);

                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);
                generalResponse.Data = await (new LeadsListBLL()).GetAllLeads(mKey,loginId, lee);

                return generalResponse;
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/pagewise-leads");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                return generalResponse;
            }
        }

    }
}
