using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.HR
{
    [DataContract]
    [Serializable]
    public class AttendanceResponseModel
    {
        [DataMember]
        public string EmpCode { get; set; }

        [DataMember]
        public Boolean Status { get; set; } // true, false

        [DataMember]
        public int ErrorCode { get; set; } // 200 for Success, 417 for Failure

        [DataMember]
        public string Message { get; set; }
    }
}
