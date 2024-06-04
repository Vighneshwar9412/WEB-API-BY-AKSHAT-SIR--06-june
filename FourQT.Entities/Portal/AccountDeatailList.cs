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
    public class AccountDeatailList
    {
        [DataMember]
        public string TagName { get; set; }
        [DataMember]
        public string TagValue { get; set; }

        [DataMember]
        public string TagColor { get; set; }
        [DataMember]
        public string TagStyle { get; set; }
        [DataMember]
        public List<UnitCost> UnitCostList { get; set; }
        [DataMember]
        public List<ExtraCharge> ExtraChargeList { get; set; }

        //[DataMember]
        //public List<TotalOutStanding> TotalOutStandingList { get; set; }
    }

    public class AccountDetail_New_Wrap { 
        public List<AccountDetails_New_Charge>? ChargeList { get; set; } = new List<AccountDetails_New_Charge>();
        public List<AccountDetails_New_Charge>? addonChargeList { get; set; } = new List<AccountDetails_New_Charge>();
        public string? totalArea { get; set; }
        public string? totalAmount { get; set; }
        public string? totalAmountWords { get; set; }
        public Boolean showAddonTable { get; set; }
        public string? addonTableName { get; set; }
        public string? totalAddonAmount { get; set; }
        public string? totalAddonAmountWords { get; set; }
    }

    public class AccountDetails_New_Charge 
    { 
        public string? particular { get; set; }
        public string? type { get; set; }
        public string? quantity { get; set; }
        public string? rate { get; set; }
        public string? amount { get; set; }
        public int rank { get; set; }
        public Boolean isFooter { get; set; }
    }

    public class PortalExtraInfo_Wrap { 
        public string? supportNo { get; set; }
    }
}
