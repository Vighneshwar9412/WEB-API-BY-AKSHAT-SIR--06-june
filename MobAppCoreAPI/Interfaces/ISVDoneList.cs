namespace MobAppCoreAPI.Interfaces
{
    public interface ISVDoneList
    {
        Task<dynamic> listsvsiteVisit(HttpRequest req,int enq_ID);
    }
}
