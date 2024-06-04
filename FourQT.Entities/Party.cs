using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class Party
    {
        public int Party_Id { get; set; }
        //[Required]
        public string? Salutation { get; set; }
        //[Required]
        public string? FirstName { get; set; }
        //[Required]
        public string? LastName { get; set; }

        public string? Display_Name { get; set; }
        //[Required]
        public string? ISD { get; set; }
        //[Required]
        public string? MobileNo1 { get; set; }

        public string? MobileNo2 { get; set; }

        public string? MobileNo3 { get; set; }
        //[Required]
        public string? EmailID1 { get; set;}

        public string? EmailID2 { get; set;}
        
        public string? DOB { get; set;}
        
        public string? DOA { get; set; }

        public string? Display_Mobile { get; set; }

    }

    public interface iPartyDisplay
    {
        public string? displayName { get; set; }

        public string? displayMobile { get; set; }
    }

    public interface iPartyCore
    {
        public string? salutation { get; set; }

        public string? firstName { get; set; }

        public string? lastName { get; set; }

        public string? emailId1 { get; set; }
    }

    public interface iPartyImp : iPartyCore
    {
        public string? isd { get; set; }

        public string? mobileNo1 { get; set; }

    }

    public interface iPartyAdditional
    {
        public int enquiryId { get; set; }

        public string? mobileNo2 { get; set; }

        public string? mobileNo3 { get; set; }

        public string? emailId2 { get; set; }

        public string? dOB { get; set; }

        public string? dOA { get; set; }

        public int channelId { get; set; }

    }

    public class PartyUpdate : iPartyCore, iPartyAdditional
    {
        public int enquiryId { get; set; }
        public string? salutation { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? emailId1 { get; set; }       
        public string? mobileNo2 { get; set; }
        public string? mobileNo3 { get; set; }
        public string? emailId2 { get; set; }
        public string? dOB { get; set; }
        public string? dOA { get; set; }
        public int channelId { get; set; }
    }

    public class PartyShort : iPartyImp
    {
        public string? salutation { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? isd { get; set; }
        public string? mobileNo1 { get; set; }
        public string? emailId1 { get; set; }
    }

    public class PartyLong : iPartyImp, iPartyAdditional,iPartyDisplay
    {
        public string? salutation { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? isd { get; set; }
        public string? mobileNo1 { get; set; }
        public string? emailId1 { get; set; }
        public int enquiryId { get; set; }
        public string? displayName { get; set; }
        public string? mobileNo2 { get; set; }
        public string? mobileNo3 { get; set; }
        public string? emailId2 { get; set; }
        public string? dOB { get; set; }
        public string? dOA { get; set; }
        public string? displayMobile { get; set; }
        public int channelId { get; set; }
    }
}

