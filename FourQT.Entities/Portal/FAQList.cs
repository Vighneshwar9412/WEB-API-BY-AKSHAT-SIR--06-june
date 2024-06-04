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
   public class FAQList
    {
        [DataMember]
        public string FA_Id { get; set; }
        [DataMember]
        public string Category_Id { get; set; }
        [DataMember]
        public string Question { get; set; }
        [DataMember]
        public string Answer { get; set; }
         [DataMember]
        public string status { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public string date { get; set; }
        [DataMember]
        public string CategoryName { get; set; }
    }
}
