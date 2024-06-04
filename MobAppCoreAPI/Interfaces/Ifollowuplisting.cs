namespace MobAppCoreAPI.Interfaces
{
    public interface IFollowUplisting
    {

        Task<dynamic> followupListing(HttpRequest req, int enq_ID); 
        public Task<dynamic> getTodayFollowup_Notification(HttpRequest req);
    }
}
