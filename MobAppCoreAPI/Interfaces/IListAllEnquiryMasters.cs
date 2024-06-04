using FourQT.Entities;

namespace MobAppCoreAPI.Interfaces
{
    public interface IListAllEnquiryMasters
    {
        Task<dynamic> listAllEnquiryMasters(HttpRequest req,int typeId);
        public Task<dynamic> enquiryMastersByEnqId(HttpRequest req, MastersByEnquiryReq model);
    }
}
