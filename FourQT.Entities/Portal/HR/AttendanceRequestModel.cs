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
    public class AttendanceRequestModel
    {

        [DataMember]
        public List<AttendanceList> AttendanceList { get; set; }
    }
}
