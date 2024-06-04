using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.General
{
    public class ForgotPasswordRequest
    {
        public string? commonLoginType { get; set; }
        public string? token { get; set; }
        public string? username { get; set; }
        //public string? emailId { get; set; }
        public string? mobile { get; set; }
    }

    public class ForgotPassEmail
    {
        public string? smtpServer { get; set; }
        public string? emailId { get; set; }
    }
}
