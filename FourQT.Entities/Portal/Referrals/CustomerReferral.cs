using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Portal.Referrals
{
    [DataContract]
    [Serializable]
    public class CustomerReferral : CustomerLogin
    {
        [DataMember]
        public int Customer_Referral_Id { get; set; }

        [DataMember]
        public string Referral_Name { get; set; }

        [DataMember]
        public string Referral_Mobile { get; set; }

        [DataMember]
        public string Referral_Mobile2 { get; set; }

        [DataMember]
        public string Referral_Email { get; set; }

        [DataMember]
        public string Referral_Address { get; set; }

        [DataMember]
        public string Referral_Relation_Remarks { get; set; }

        [DataMember]
        public int Referral_Project_Id { get; set; }

        [DataMember]
        public int CP_Id { get; set; }

        [DataMember]
        public int Registration_Id { get; set; }

        [DataMember]
        public string Action { get; set; }

        [DataMember]
        public string Customer_Name { get; set; }

        [DataMember]
        public string Project_Name { get; set; }

        [DataMember]
        public string Display_Create_Date { get; set; }

        [DataMember]
        public string Referral_Location { get; set; }
    }
}
