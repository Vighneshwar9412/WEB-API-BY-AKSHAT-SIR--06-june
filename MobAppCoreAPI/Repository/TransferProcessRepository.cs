using FourQT.CommonFunctions;
using FourQT.Core;
using FourQT.Entities;
using FourQT.Reports;
using MobAppCoreAPI.Interfaces;
using System.Net;
using FourQT.Utilities;
using FourQT.CommonFunctions.Portal;
using Newtonsoft.Json;

namespace MobAppCoreAPI.Repository
{
    public class TransferProcessRepository:ITransferProcess
    {
        public async Task<dynamic> transferprocess(HttpRequest req, Transfer prr, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(prr);
                Log.LogPayloadDateWise(message, "TransferProcess", context);

                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);
                generalResponse = (APIObjectResponse)(new TransferProcessBLL()).transferprocess(mKey,  prr, loginId);

                return generalResponse;
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "v1/transfer-process");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                return generalResponse;
            }
        }

        public async Task<dynamic> bulktransferprocess(HttpRequest req, Transfer prr, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(prr);
                Log.LogPayloadDateWise(message, "bulktransferprocess", context);

                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);
                generalResponse = (APIObjectResponse)(new TransferProcessBLL()).BulkTransferProcess(mKey, prr, loginId);

                return generalResponse;
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "v1/bulktransfer-process");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                return generalResponse;
            }
        }
    }
}
