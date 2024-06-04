using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Portal
{
    [DataContract]
    [Serializable]
    public class CustomerNameMobile
    {
        [DataMember]
        public string ProjectType { get; set; }
        [DataMember]
        public string F_First_name { get; set; }
        [DataMember]
        public string F_R_Mobile_no { get; set; }
        [DataMember]
        public string F_EMail_id { get; set; }
        [DataMember]
        public string F_H_Mobile_no { get; set; }
        [DataMember]
        public int Registration_Id { get; set; }
        [DataMember]
        public int CP_Id { get; set; }


    }
}
