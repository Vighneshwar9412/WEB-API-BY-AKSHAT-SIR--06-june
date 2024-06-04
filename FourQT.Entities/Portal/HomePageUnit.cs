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
    public class HomePageUnit
    {
        [DataMember]
        public int Registration_Id { get; set; }
        [DataMember]
        public string? Project { get; set; }
        [DataMember]
        public string? UnitNo { get; set; }
        [DataMember]
        public string? Dues { get; set; }
    }
    public class HomePageUnitNew : HomePageUnit
    {
        public string? unitImage { get; set; }
        public string? unitIcon { get; set; }
    }
}
