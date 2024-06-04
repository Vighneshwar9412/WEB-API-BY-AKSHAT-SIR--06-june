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
    public class InsertqueryHistory
    {
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public int queryid { get; set; }
        [DataMember]
        public string text { get; set; }
        [DataMember]
        public int createdby { get; set; }

        [DataMember]
        public string mailstatus { get; set; }
        [DataMember]
        public int regid { get; set; }

        [DataMember]
        public string RootDomain { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string FileContent { get; set; }

    }

    public class InsertqueryHistoryNew
    {
        [DataMember]
        public int queryid { get; set; }
        [DataMember]
        public string? text { get; set; }
        [DataMember]
        public int createdby { get; set; }

        [DataMember]
        public string? mailstatus { get; set; }
        [DataMember]
        public int regid { get; set; }

        [DataMember]
        public string? RootDomain { get; set; }

        [DataMember]
        public string? FileName { get; set; }

        [DataMember]
        public string? FileContent { get; set; }
    }

    public class QueryHistoryResponse
    {
        [DataMember]
        public int queryId { get; set; }

        [DataMember]
        public int registrationId { get; set; }

        [DataMember]
        public string? queryType { get; set; }

        [DataMember]
        public string? text { get; set; }

        [DataMember]
        public string? createdDate { get; set; }

        [DataMember]
        public string? status { get; set; }

        [DataMember]
        public string? customerName { get; set; }

        [DataMember]
        public string? regNo { get; set; }

        [DataMember]
        public string? projectName { get; set; }

        [DataMember]
        public string? towerName { get; set; }

        [DataMember]
        public string? unitNo { get; set; }

        [DataMember]
        public string? attachment { get; set; }

        [DataMember]
        public string? lastRemark { get; set; }

        [DataMember]
        public string? lastRemarkDate { get; set; }

        [DataMember]
        public string? lastRemarkBy { get; set; }

        [DataMember]
        public int convoCount { get; set; }
    }

    public class QueryConversationResponse
    {
        [DataMember]
        public int conversationId { get; set; }

        [DataMember]
        public string? status { get; set; }

        [DataMember]
        public string? remark { get; set; }        

        [DataMember]
        public string? responseDate { get; set; }

        [DataMember]
        public string? responseBy { get; set; }
    }
}
