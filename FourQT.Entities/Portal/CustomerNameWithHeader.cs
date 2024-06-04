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
    public class CustomerNameWithHeader
    {
        [DataMember]
        public string CustomerName { get; set; }
    }
}
