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
    public class AttendanceList
    {
        [DataMember]
        public string EmpCode { get; set; }

        [DataMember]
        public DateTime AttendanceDate { get; set; } // In yyyy-MM-dd HH:MM format

        [DataMember]
        public string AttendanceTime { get; set; } //

        [DataMember]
        public string Type { get; set; } // In, Out

        [DataMember]
        public string Remark { get; set; }
    }
}
