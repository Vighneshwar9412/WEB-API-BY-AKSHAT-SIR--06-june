namespace MobAppCoreAPI.Interfaces
{
    public interface IProjectDocs
    {
        Task<dynamic> projectdocslisting(HttpRequest req, int project_ID,int item_id);
    }
}
