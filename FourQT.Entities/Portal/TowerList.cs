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
    public class TowerList
    {
       // [DataMember]
        //public int CS_Id { get; set; }
        [DataMember]
        public string Images { get; set; }        
        [DataMember]
        public string sContent { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string ApprovedDate { get; set; }
    }
}
