using FourQT.Entities.Employee;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes.Employee;
using MobAppCoreAPI.Interfaces.Employee;
using Swashbuckle.AspNetCore.Annotations;

namespace MobAppCoreAPI.Controllers.Employee
{
    [Route("/api/e1/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "e1")]
    [APIEmployeeKey]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _IEmployee;

        public EmployeeController(IEmployee IEmployee)
        {
            _IEmployee = IEmployee;
        }

        [HttpPost]
        public async Task<dynamic> ChangePasswordEmployee([FromBody] ChangePasswordEmployeeRequest model)
        {
            return await _IEmployee.changePasswordEmployee(model, Request, HttpContext);
        }
    }
}
