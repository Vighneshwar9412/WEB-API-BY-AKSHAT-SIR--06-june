using FourQT.CommonFunctions;
using FourQT.Entities;
using FourQT.Reports;
using MobAppCoreAPI.Interfaces;
using System.Net;
using FourQT.Utilities;

namespace MobAppCoreAPI.Repository
{
    public class TransferuserlistRepository:ITransferUserList
    {
        public async Task<dynamic> transferuserlist(HttpRequest req)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            generalResponse.IsSuccess = true;
            generalResponse.Status = HttpStatusCode.OK;
            generalResponse.Message = "Success";

            try
            {
                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);

                generalResponse.Data = await (new UserListBLL()).transferuserlist(mKey, loginId);

                return generalResponse;
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/lead-icon");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                return generalResponse;
            }
        }
    }
}
