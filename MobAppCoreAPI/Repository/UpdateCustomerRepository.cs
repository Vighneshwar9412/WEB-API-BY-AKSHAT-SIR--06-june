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
using Newtonsoft.Json;
using FourQT.CommonFunctions.Portal;

namespace MobAppCoreAPI.Repository
{
    public class UpdateCustomerRepository:IUpdateCustomer
    {
        public async Task<dynamic> updatecustomer(HttpRequest req, PartyUpdate paa, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(paa);
                Log.LogPayloadDateWise(message, "UpdateCustomer", context);

                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);
                generalResponse=(APIObjectResponse) (new UpdateCustBLL()).updatecustomer(mKey, paa, loginId);

                return generalResponse;
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/update-customer");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                return generalResponse;
            }
        }
    }
}
