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
using FourQT.Entities.Employee;
using FourQT.CommonFunctions.Portal;
using Newtonsoft.Json;

namespace MobAppCoreAPI.Repository
{
    public class LeadInventoryRepository : ILeadInventory
    {
        public async Task<APIObjectResponse> getInventoryMasters(InventoryRequestMasters model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "Lead_getInventoryMasters", context);

                generalResponse = await (new MastersBLL()).getInventoryMasters(model, req, context);
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/LeadInventory/GetInventoryMasters");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Status = HttpStatusCode.BadRequest;
                generalResponse.Title = "Error";
            }

            return generalResponse;
        }

        public async Task<APIObjectResponse> getInventoryUnitStatus(InventoryRequestLong model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "Lead_getInventoryUnitStatus", context);

                generalResponse = await (new MastersBLL()).getInventoryUnitStatus(model, req, context);
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/LeadInventory/GetInventoryUnitStatus");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Status = HttpStatusCode.BadRequest;
                generalResponse.Title = "Error";
            }

            return generalResponse;
        }
    }
}
