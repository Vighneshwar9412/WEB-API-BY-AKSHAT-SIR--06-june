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
    public class LetterDetailCustomer
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string date { get; set; }
        [DataMember]
        public string Letter { get; set; }
        [DataMember]
        public string LetterLink { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string PDFLink { get; set; }
    }
}
