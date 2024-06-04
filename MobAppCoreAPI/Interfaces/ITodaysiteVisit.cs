namespace MobAppCoreAPI.Interfaces
{
    public interface ItodaysiteVisit
    {
        Task<dynamic> ListDashboardTodaySVScheduled(HttpRequest req,int page_index, int page_size);
    }
}
