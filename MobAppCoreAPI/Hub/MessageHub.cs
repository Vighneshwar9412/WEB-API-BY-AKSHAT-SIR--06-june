using Microsoft.AspNetCore.SignalR;

namespace MobAppCoreAPI.Hub
{
    public class MessageHub : Hub<IMessageHubClient>
    {
        public async Task SendAlertMessage(List<string> message)
        {
            await Clients.All.SendAlertMessage(message);
        }
    }
}
