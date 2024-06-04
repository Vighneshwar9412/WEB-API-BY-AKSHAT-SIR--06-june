using MobAppCoreAPI.Models.Response;
using FourQT.Entities;
namespace MobAppCoreAPI.Interfaces
{
    public interface IValidateKey
    {
        Task<APIObjectResponse> ValidateKey(string key);
    }
}
