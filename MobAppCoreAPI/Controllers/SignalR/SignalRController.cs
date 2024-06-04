using FourQT.Entities.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MobAppCoreAPI.Hub;
using MobAppCoreAPI.Repository.SignalR;

namespace MobAppCoreAPI.Controllers.SignalR
{
    [Route("api/SignalR")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "m1")]
    public class SignalRController : ControllerBase
    {
        private IHubContext<MessageHub, IMessageHubClient> messageHub;
        public SignalRController(IHubContext<MessageHub, IMessageHubClient> _messageHub)
        {
            messageHub = _messageHub;
        }

        [HttpPost]
        [Route("SendAlertMessage")]
        public string SendAlert(string connectionId)
        {
            List<string> offers = new List<string>();
            offers.Add("20% Off on IPhone 12");
            offers.Add("15% Off on HP Pavillion");
            offers.Add("25% Off on Samsung Smart TV");

            if (connectionId != null && connectionId.Trim() != "A") {
                messageHub.Clients.Client(connectionId.Trim()).SendAlertMessage(offers);
            }
            else { 
                messageHub.Clients.All.SendAlertMessage(offers);
            }

            return "Offers sent successfully to all users!";
        }

        [HttpPost]
        [Route("SendNotification")]
        public async Task<dynamic> SendNotification(SignalRRequest request)
        {
            return await (new SignalRRepository()).sendNotification(request);
        }
    }
}
