using FourQT.Entities.Employee;
using FourQT.Masters;
using FourQT.UserRights;
using MobAppCoreAPI.Interfaces.Employee;

namespace MobAppCoreAPI.Repository.Employee
{
    public class EmployeeRepository : IEmployee
    {
        public async Task<dynamic> changePasswordEmployee(ChangePasswordEmployeeRequest model, HttpRequest req, HttpContext context)
        {
            return await (new EmployeeUserRightsBLL()).changePasswordEmployee(model,req,context);
        }
    }
}
