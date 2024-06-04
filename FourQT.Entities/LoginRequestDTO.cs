using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class LoginRequestDTO
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string? deviceType { get; set; }
        public string? fcmToken { get; set; }

        public string? ipAddress { get; set; }

        public string? location { get; set; }
    }
}
