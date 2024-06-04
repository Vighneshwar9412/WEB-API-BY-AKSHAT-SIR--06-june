namespace MobAppCoreAPI.Hub
{
    public interface IMessageHubClient
    {
        Task SendAlertMessage(List<string> message);
    }
}

