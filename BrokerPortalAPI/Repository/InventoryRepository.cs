using BrokerPortalAPI.Interfaces;
using FourQT.CommonFunctions;
using FourQT.Entities;
using FourQT.CommonFunctions;
using FourQT.Entities;
using FourQT.Reports;
using BrokerPortalAPI.Interfaces;
using System.Net;
using FourQT.Utilities;
using FourQT.Core;


namespace BrokerPortalAPI.Repository
{
    public class InventoryRepository:IInventory
    {

        public async Task<dynamic> getInventoryList(HttpRequest req, int projectId,int towerId,string type)
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
                generalResponse.Data = (new InventoryBLL()).getInventoryList(mKey, loginId, projectId,towerId,type);

                return generalResponse;
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/followup-listing");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                return generalResponse;
            }
        }


       
    }
}
