using MobAppCoreAPI.Models.Response;
using FourQT.Entities;

namespace MobAppCoreAPI.Interfaces
{
    public interface IGetLeadDetails
    {
        public Task<dynamic> GetLeadDetails(HttpRequest req, int enquiryId);
    }
}
