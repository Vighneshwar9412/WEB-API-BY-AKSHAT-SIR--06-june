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
    public class CustomerDuesPaid
    {
        [DataMember]
        public int Registration_Id { get; set; }

        [DataMember]
        public string? TotalCost { get; set; }

        [DataMember]
        public string? TotalDue { get; set; }

        [DataMember]
        public string? TotalReceived { get; set; }

        [DataMember]
        public string? TotalOutstanding { get; set; }

        [DataMember]
        public string? TotalUndue { get; set; }


        [DataMember]
        public decimal TotalDue_Per { get; set; }

        [DataMember]
        public decimal TotalReceived_Per { get; set; }

        [DataMember]
        public decimal TotalOutstanding_Per { get; set; }

        [DataMember]
        public decimal TotalUndue_Per { get; set; }

        [DataMember]
        public string? TotalCost_Color { get; set; }

        [DataMember]
        public string? TotalDue_Color { get; set; }

        [DataMember]
        public string? TotalReceived_Color { get; set; }

        [DataMember]
        public string? TotalOutstanding_Color { get; set; }

        [DataMember]
        public string? TotalUndue_Color { get; set; }

        [DataMember]
        public string? TotalInterest { get; set; }

        [DataMember]
        public Boolean showInterest { get; set; }

        [DataMember]
        public Boolean showAddon { get; set; }

        [DataMember]
        public string? addonDueAmt { get; set; }

        [DataMember]
        public string? addonDueLabel { get; set; }

        [DataMember]
        public string? totalDueWithAddon { get; set; }

        [DataMember]
        public string? totalDueWithAddonLabel { get; set; }

    }
}
