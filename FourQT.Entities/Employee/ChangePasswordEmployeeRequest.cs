using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Employee
{
    public class ChangePasswordEmployeeRequest
    {
        public string? oldPassword { get; set; }
        public string? newPassword { get; set; }
        public string? confirmNewPassword { get; set; }
    }

    public class ChangePasswordLeadRequest : ChangePasswordEmployeeRequest 
    { 
    }
}
