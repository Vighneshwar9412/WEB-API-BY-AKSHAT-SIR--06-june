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
    public class HomePagesIcon
    {
        [DataMember]
        public string? Icon { get; set; }

        [DataMember]
        public string? Name { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public string? moduleType { get; set; }

        [DataMember]
        public string? moduleSubType { get; set; }

        [DataMember]
        public string? link { get; set; }
    }
}
