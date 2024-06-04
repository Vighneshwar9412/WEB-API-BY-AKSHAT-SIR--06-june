using FourQT.Entities;
using FourQT.Entities.General;
using FourQT.Entities.Portal;

namespace MobAppCoreAPI.Interfaces.General
{
    public interface IGeneralHomepage
    {
        public Task<APIObjectResponse> getGeneralHomepageContent(string dKey); 
        public Task<APIObjectResponse> getMultipleProjectKeys(string dKey);
    }
}
