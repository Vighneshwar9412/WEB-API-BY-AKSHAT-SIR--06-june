using System.Runtime.Serialization;

namespace FourQT.Entities
{
    [DataContract]
    [Serializable]
    public class CustomerDetail
    {
        [DataMember]
        public string PrjectName { get; set; }
        [DataMember]
        public int RegistrationId { get; set; }
        [DataMember]
        public string? RegistrationNo { get; set; }
        [DataMember]
        public string Project_Tower_Name { get; set; }
        [DataMember]
        public string Project_Tower_Floor_Name { get; set; }
        [DataMember]
        public string location { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Registration_Date { get; set; }
        [DataMember]
        public string F_First_name { get; set; }
        [DataMember]
        public string ApplicantAddress { get; set; }
        [DataMember]
        public string S_First_name { get; set; }
        [DataMember]
        public string SecondApplicantAddress { get; set; }
        [DataMember]
        public string S_Pan_No { get; set; }
        [DataMember]
        public string F_Pan_No { get; set; }
        [DataMember]
        public string Plan_Name { get; set; }

        [DataMember]
        public string F_Date_of_Birth { get; set; }

        [DataMember]
        public string S_Date_of_Birth { get; set; }

        [DataMember]
        public string LoanInfo { get; set; }
        [DataMember]
        public string Bank_Name { get; set; }
        [DataMember]
        public string F_Passport_No { get; set; }

        [DataMember]
        public string S_Passport_No { get; set; }

        [DataMember]
        public string F_Nationality { get; set; }
        [DataMember]
        public string S_Nationality { get; set; }

        [DataMember]
        public string Project_Unit_Type_Name { get; set; }

        [DataMember]
        public string Area_Measurement { get; set; }
        [DataMember]
        public string Project_Areameasurement { get; set; }
        [DataMember]
        public string F_MobileNo { get; set; }
        [DataMember]
        public string S_MobileNo { get; set; }
        [DataMember]
        public string F_LandLineNo { get; set; }
        [DataMember]
        public string S_LandLineNo { get; set; }
        [DataMember]
        public string F_Email_id { get; set; }
        [DataMember]
        public string S_Email_id { get; set; }

        [DataMember]
        public string F_R_Address { get; set; }


        [DataMember]
        public string F_O_Address { get; set; }
        [DataMember]
        public string F_H_Address { get; set; }
        [DataMember]
        public string MailingAddress { get; set; }
        [DataMember]
        public string S_R_Address { get; set; }
        [DataMember]
        public string S_O_Address { get; set; }
        [DataMember]
        public string S_H_Address { get; set; }
        [DataMember]
        public string Allotment_Date { get; set; }
        [DataMember]
        public string MultiApplicants { get; set; }
        [DataMember]
        public string SecAppType { get; set; }

        [DataMember]
        public string F_S_W_D_O { get; set; }
        [DataMember]
        public string Aadhaar_No { get; set; }
        [DataMember]
        public string F_Profession { get; set; }
        [DataMember]
        public string F_Company_Firm_Name { get; set; }

        [DataMember]
        public string S_S_W_D_O { get; set; }
        [DataMember]
        public string SecondApplicant_Aadhaar_No { get; set; }
        [DataMember]
        public string S_Profession { get; set; }
        [DataMember]
        public string S_Company_Firm_Name { get; set; }





    }
}
