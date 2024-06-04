using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Firebase
{
    public class Notification_Device
    {
        public string MessageTitle { get; set; }
        public string MessageBody { get; set; }
        public string multicast_id { get; set; }
        public string success { get; set; }
        public string failure { get; set; }
        public string canonical_ids { get; set; }
        public List<Result> results { get; set; }
    }
}
