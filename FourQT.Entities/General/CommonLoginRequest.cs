using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.General
{
    public class CommonLoginRequest
    {
        public string? type { get; set; }
        public string? Token { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }       
        public string? DeviceId { get; set; }
        public int FromDevice { get; set; }
        public string? Location { get; set; }
        public string? ipAddress { get; set; }

    }
}
