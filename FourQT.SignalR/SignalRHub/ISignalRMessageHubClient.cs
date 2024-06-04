using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.SignalR.SignalRHub
{
    public interface ISignalRMessageHubClient
    {
        Task SendSignalRNotification(string message);
    }
}
