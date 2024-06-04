using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Employee
{
    public class EmployeeLoginRequest
    {
        public string? token { get; set; }
        public string? userName { get; set; }
        public string? password { get; set; }
        public string? deviceType { get; set; }
        public string? fcmToken { get; set; }
        public string? ipAddress { get; set; }
        public string? location { get; set; }
    }

    public class EmployeeLoginResponse
    {
        public string? token { get; set; }
        public EmployeeDetails empDetails { get; set; } = new EmployeeDetails(); 

    }
}
