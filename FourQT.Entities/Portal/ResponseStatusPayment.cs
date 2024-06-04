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
    public class ResponseStatusPayment<T> : ResponseStatus<T>
    {
        [DataMember]
        public string TotalInstallmentTagName { get; set; }
        [DataMember]
        public string TotalInstallmentTagValue { get; set; }

        [DataMember]
        public string TotalInstallmentColor { get; set; }
        [DataMember]
        public string TotalInstallmentStyle { get; set; }
    }

    public class ResponseStatusPayment_New<T> : ResponseStatus_New<T>
    {
        [DataMember]
        public string? TotalInstallmentTagName { get; set; }

        [DataMember]
        public string? TotalInstallmentTagValue { get; set; }

        [DataMember]
        public string? TotalInstallmentColor { get; set; }

        [DataMember]
        public string? TotalInstallmentStyle { get; set; }
    }
}
