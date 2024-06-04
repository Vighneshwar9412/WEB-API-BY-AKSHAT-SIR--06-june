namespace MobAppCoreAPI.Interfaces
{
    public interface IGetTeamWiseEmployees
    {
        Task<dynamic> GetTeamWiseEmployees(HttpRequest req);
    }
}
