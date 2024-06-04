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
    public class ResponseStatusRecipt<T> : ResponseStatus<T>
    {
        [DataMember]
        public string TotalReceiptAmountTagName { get; set; }

        [DataMember]
        public string TotalReceiptAmountTagValue { get; set; }

        [DataMember]
        public string TotalReceiptTagNameColor { get; set; }

        [DataMember]
        public string TotalReceiptTagNameStyle { get; set; }
    }

    public class ResponseStatusRecipt_New<T> : ResponseStatus_New<T>
    {
        [DataMember]
        public string? TotalReceiptAmountTagName { get; set; }

        [DataMember]
        public string? TotalReceiptAmountTagValue { get; set; }

        [DataMember]
        public string? TotalReceiptTagNameColor { get; set; }

        [DataMember]
        public string? TotalReceiptTagNameStyle { get; set; }
    }
}
