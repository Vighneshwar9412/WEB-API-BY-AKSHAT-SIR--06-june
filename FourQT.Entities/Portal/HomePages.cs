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
    public class HomePages
    {

        [DataMember]
        public List<HomePageDisplay>? DisplayList { get; set; }
        [DataMember]
        public List<HomePageUnit>? UnitList { get; set; }
        [DataMember]
        public List<HomePagesIcon>? IconList { get; set; }

        [DataMember]
        public List<CustomerMessage>? MessageList { get; set; }

        [DataMember]
        public List<CustomerNameWithHeader>? CustomerNameList { get; set; }

    }

    public class HomePagesNew {
        [DataMember]
        public List<HomePageDisplay>? DisplayList { get; set; }
        [DataMember]
        public List<HomePageUnitNew>? UnitList { get; set; }
        [DataMember]
        public List<HomePagesIcon>? IconList { get; set; }

        [DataMember]
        public List<CustomerMessage>? MessageList { get; set; }

        [DataMember]
        public List<CustomerNameWithHeader>? CustomerNameList { get; set; }
    }

}
