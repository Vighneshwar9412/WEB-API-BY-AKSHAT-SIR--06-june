using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.SignalR.SignalRHub
{
    public class SignalRMessageHub : Hub<ISignalRMessageHubClient>
    {
        public async Task SendSignalRNotification(string message)
        {
            await Clients.All.SendSignalRNotification(message);
        }
    }
}
