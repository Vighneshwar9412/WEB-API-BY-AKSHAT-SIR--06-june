using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FourQT.Entities.Portal;

namespace FourQT.Entities
{
    [DataContract]
    [Serializable]
    public class CustomerDocument : CustomerLogin
    {
        [DataMember]
        public int DocumentId { get; set; }
        [DataMember]
        public int PersonalDocumentId { get; set; }
        [DataMember]
        public string DocType { get; set; }
    }

    [DataContract]
    [Serializable]
    public class ViewCustomerDocument
    {
        [DataMember]
        public int DocumentId { get; set; }
        [DataMember]
        public string DocumentName { get; set; }
        [DataMember]
        public int PersonalDocumentId { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string FullFilePath { get; set; }
        [DataMember]
        public string Type { get; set; }
    }
}
