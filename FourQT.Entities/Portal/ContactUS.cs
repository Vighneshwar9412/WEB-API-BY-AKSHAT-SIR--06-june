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
    public class ContactUS
    {
        [DataMember]
        public string Mobile_Number { get; set; }
        [DataMember]
        public string HTMLString { get; set; }
    }
}
