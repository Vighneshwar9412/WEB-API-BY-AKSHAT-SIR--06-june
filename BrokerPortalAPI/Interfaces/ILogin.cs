using BrokerPortalAPI.Models.Request;
using BrokerPortalAPI.Models.Response;
using FourQT.Entities;
using System.Threading.Tasks;

namespace BrokerPortalAPI.Interfaces
{
    public interface ILogin
    {
        Task<APIObjectResponse> LoginAsync(LoginRequestDTO loginRequestDTO, string mkey);
    }
}
