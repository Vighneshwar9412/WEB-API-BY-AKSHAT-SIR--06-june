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
    public class HomePageDisplay
    {
        [DataMember]
        public String Display { get; set; }

        [DataMember]
        public string Types { get; set; }
        [DataMember]
        public int Postion { get; set; }
    }
}
