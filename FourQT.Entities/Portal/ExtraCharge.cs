using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    [DataContract]
    [Serializable]
    public class ExtraCharge
    {

        //[DataMember]
        //public string basicName { get; set; }
        //[DataMember]
        //public string basicvalue { get; set; }
        //[DataMember]      
        //public string basicNameColor { get; set; }
        //[DataMember]
        //public string basicNameStyle { get; set; }

        public string headingName { get; set; }
        [DataMember]
        public string headingvalue { get; set; }
        [DataMember]
        public string headingNameColor { get; set; }
        [DataMember]
        public string headingNameStyle { get; set; }
        [DataMember]
        public List<TagModalList> ContentList { get; set; }
    }
}
