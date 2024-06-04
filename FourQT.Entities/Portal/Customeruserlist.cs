using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using FourQT.Entities.General;

namespace FourQT.Entities.Portal
{
    [DataContract]
    [Serializable]
    public class Customeruserlist
    {
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public string Company_Logo { get; set; }
        [DataMember]
        public string Profile_Photo { get; set; }
    }
}
