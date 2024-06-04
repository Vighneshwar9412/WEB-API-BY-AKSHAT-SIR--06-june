using MobAppCoreAPI.Models.Request;
using MobAppCoreAPI.Models.Response;
using FourQT.Entities;
using FourQT.Entities.Employee;

namespace MobAppCoreAPI.Interfaces
{
    public interface ILogin
    {
        Task<dynamic> LoginAsync(LoginRequestDTO loginRequestDTO, string mkey );        
    }
}
