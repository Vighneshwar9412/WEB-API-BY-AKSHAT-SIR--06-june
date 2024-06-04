using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class Lead : Party
    {
        
        public int Enquiry_Id { get; set; }
       // [Required]
        public int sourceId { get; set; }
        
        public string? Display_EnquiryDate { get; set; }
        
        public string? EnquiryType { get; set; }

        public string? Display_Status { get; set; }

        public int ChannelId { get; set; }

        public int DumpId { get; set; }

        //public string FollowType { get; set; }

        public int CId { get; set; }

        public string? Remarks { get; set; }
        
        public string? CallDirection { get; set; }

        public int HandleBy_Id { get; set; }

        public int Owner_Id { get; set; }

        public string? Project_Name { get; set; }
    }

    public interface iLeadDump
    {
        public int dumpId { get; set; }

        public int cId { get; set; }

    }

    public interface iLeadImp
    {
        public int sourceId { get; set; }
        public string? enquiryType { get; set; }
        public string? callDirection { get; set; }
        public string? remarks { get; set; }
    }

    public interface iLeadAdditional : iLeadDump
    {
        public int enquiryId { get; set; }

        public int channelId { get; set; }

        public string? displayEnquiryDate { get; set; }

        public string? displayStatus { get; set; }       

        public int dumpId { get; set; }

        public int cId { get; set; }      

        public int handlebyId { get; set; }

        public int ownerId { get; set; }

        public string? projectName { get; set; }
    }

    public class LeadShort : iLeadImp, iPartyImp
    {
        public int sourceId { get; set; }
        public string? enquiryType { get; set; }
        public string? callDirection { get; set; }
        public string? salutation { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? isd { get; set; }
        public string? mobileNo1 { get; set; }
        public string? emailId1 { get; set; }
        public int channelId { get; set; }
        public string? remarks { get; set; }
    }

    public class LeadLong : iLeadImp, iLeadAdditional, iPartyImp, iPartyAdditional,iPartyDisplay
    {
        public int sourceId { get; set; }
        public string? enquiryType { get; set; }
        public string? callDirection { get; set; }
        public int enquiryId { get; set; }
        public string? displayEnquiryDate { get; set; }
        public string? displayStatus { get; set; }
        public int channelId { get; set; }
        public int dumpId { get; set; }
        public int cId { get; set; }
        public string? remarks { get; set; }
        public int handlebyId { get; set; }
        public int ownerId { get; set; }
        public string? projectName { get; set; }
        public string? salutation { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? isd { get; set; }
        public string? mobileNo1 { get; set; }
        public string? emailId1 { get; set; }
        public int partyId { get; set; }
        public string? displayName { get; set; }
        public string? mobileNo2 { get; set; }
        public string? mobileNo3 { get; set; }
        public string? emailId2 { get; set; }
        public string? dOB { get; set; }
        public string? dOA { get; set; }
        public string? displayMobile { get; set; }
    }

    public class MastersByEnquiryReq
    {
        public int enquiryId { get; set; }
    }

}
