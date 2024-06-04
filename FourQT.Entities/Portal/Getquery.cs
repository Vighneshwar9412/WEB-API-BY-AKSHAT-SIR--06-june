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
    public class Getquery
    {
        [DataMember]
        public string Query_Name { get; set; }
        [DataMember]
        public int Q_id { get; set; }
    }

    [DataContract]
    [Serializable]
    public class QueryDetailsRequest : CustomerCore2
    {
        [DataMember]
        public string? detailType { get; set; }

        [DataMember]
        public int queryId { get; set; }
    }
}
