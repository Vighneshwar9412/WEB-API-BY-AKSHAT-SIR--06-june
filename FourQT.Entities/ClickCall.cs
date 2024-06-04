using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class ClickCall
    {
        public string? DocNo { get; set; } // Doc No (Getting from login)
        public string? customerNumber { get; set; } // Customer Number
        public string? agentNumber { get; set; } // Agent No (Getting from login)
        //public string Method { get; set; }
        //public string OtherKeys { get; set; }
        public int EnquiryId { get; set; }
        public string? CallerId { get; set; } // Caller_Id (Getting from login)
    }

    public class KPostiveResponse
    {
        public KSuccessFailure success { get; set; }
    }

    public class KNegativeResponse
    {
        public KSuccessFailure error { get; set; }
    }

    public class KSuccessFailure
    {
        public string status { get; set; }
        public string message { get; set; }
        public string call_id { get; set; }
    }

    public class MCubeResponse
    {
        public string msg { get; set; }
        public string callid { get; set; }
    }
}
