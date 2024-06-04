using FourQT.Entities.Employee;

namespace MobAppCoreAPI.Interfaces.Employee
{
    public interface IEmployee
    {
        public Task<dynamic> changePasswordEmployee(ChangePasswordEmployeeRequest model, HttpRequest req,HttpContext context);
    }
}
