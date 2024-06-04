using FourQT.Entities.Employee;

namespace MobAppCoreAPI.Interfaces
{
    public interface ILogout
    {
        Task<dynamic> logout(HttpRequest req);
        public Task<dynamic> changePassword(ChangePasswordLeadRequest model, HttpRequest req, HttpContext context);
    }
}
