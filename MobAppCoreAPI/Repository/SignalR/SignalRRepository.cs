using FourQT.Entities;
using FourQT.Entities.SignalR;
using FourQT.SignalR;

namespace MobAppCoreAPI.Repository.SignalR
{
    public class SignalRRepository
    {
        public async Task<dynamic> sendNotification(SignalRRequest request)
        {
            APIObjectResponse genResponse = new APIObjectResponse();

            try {
                //genResponse = await (new SignalRBLL()).sendNotification(request);
            }
            catch(Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.Message;
                genResponse.Title = "Error";
                genResponse.Status = System.Net.HttpStatusCode.BadRequest;
            }

            return genResponse;
        }
    }
}
