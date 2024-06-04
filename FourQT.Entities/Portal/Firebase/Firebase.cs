using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Firebase
{
    public class Device
    {
        public string Value { get; set; }
    }
    public class Firebase
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string ServerKey { get; set; }
        public string SenderId { get; set; }
        public int ActionId { get; set; }
        public int RegistrationId { get; set; }
        public string DocumentId { get; set; }
        public string Type { get; set; }
        public List<Device> DeviceKeys { get; set; }
    }
}
