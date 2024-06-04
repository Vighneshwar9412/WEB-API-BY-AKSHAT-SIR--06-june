namespace MobAppCoreAPI.Interfaces
{
    public interface IDashboard_GetTodayLeads
    {
        public Task<dynamic> getTodayLeads(HttpRequest req,int page_Index,int pge_size);
    }
}
