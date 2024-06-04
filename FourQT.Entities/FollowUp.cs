using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class FollowUpSave : Lead
    {
        public int FollowedBy { get; set; }

        public int Followup_Channel_Id { get; set; }

        public string? CallDirection { get; set; }

        public int Response_Id { get; set; }

        public int SubResponse_Id { get; set; }

        public string? FollowType { get; set; }

        public string? EnquiryType { get; set; }

        public DateTime? NextFollowupDate { get; set; }

        public string? Time { get; set; }

        public string? TimeFormat { get; set; }

        //public int CID { get; set; }

        public int TimeFrameId { get; set; }

        public int StageId { get; set; }

        public DateTime? MeetingDate { get; set; }

        public string? MeetingTime { get; set; }

        public int MeetingDurationInMin { get; set; }

        // public string MeetingAddress { get; set; }

        //  public string MeetingPlace { get; set; }

        public string? Stage_Project_Id { get; set; }

        public string? Remarks { get; set; }

        public string? Booking_Data { get; set; }

        public string? Project_Ids { get; set; }

        public int TransferTo { get; set; }

        public string? TransferAutoManual { get; set; }

    }

    public interface iFollowUpCore
    {
        public int enquiryId { get; set; }

        public int channelId { get; set; }

        public string? remarks { get; set; }
    }

    public interface iFollowUpProject
    {
        public int projectId { get; set; }
        public int projectUnitTypeId { get; set; }
        public string? tower { get; set; }
        public string? floor { get; set; }
        public string? unitNo { get; set; }
        public int paymentPlanId { get; set; }
        public int cId { get; set; }
    }

    public interface iTransfer
    {
        public int TransferTo { get; set; }
    }

    public class ShortFollowUpSave : iFollowUpCore
    {
        public int enquiryId { get; set; }

        public int enquiryTypeId { get; set; }

        public int subResponseId { get; set; }

        public int cId { get; set; }

        public string? nextFollowupDate { get; set; }

        //public string? nextFollowupTime { get; set; }

        //public string? nextFollowupTimeFormatAMPM { get; set; }

        public int siteVisitDurationInMin { get; set; }

        public string? siteVisitDate { get; set; }

        //public string? siteVisitTime { get; set; }

        //public string? siteVisitTimeFormatAMPM { get; set; }

        public string? siteVisitAddress { get; set; }

        public int? timeFrameId { get; set; }

        public int stageProjectId { get; set; }

        public int channelId { get; set; }

        public string? remarks { get; set; }

        public string? meetingPlace { get; set; }
    }

    public class Dump : iLeadDump, iFollowUpCore
    {
        public int dumpId { get; set; }
        public int cId { get; set; }
        public int enquiryId { get; set; }
        public int channelId { get; set; }
        public string? remarks { get; set; }
    }

    public class Transfer : iTransfer, iFollowUpCore
    {
        public int TransferTo { get; set; }
        public int enquiryId { get; set; }
        public int channelId { get; set; }
        public string? remarks { get; set; }
        public string? commaSeparatedEnquiryIds { get; set; }
    }

    public class LeadSuccess : iFollowUpCore, iFollowUpProject
    {
        public int enquiryId { get; set; }
        public int channelId { get; set; }
        public string? remarks { get; set; }
        public int projectId { get; set; }
        public int projectUnitTypeId { get; set; }
        public string? tower { get; set; }
        public string? floor { get; set; }
        public string? unitNo { get; set; }
        public int paymentPlanId { get; set; }
        public int cId { get; set; }
        public string? area { get; set; }
        public string? areaUnit { get; set; }
        public decimal? price { get; set; }
        public string? customerName { get; set; }
        public string? customerMobile { get; set; }
        public DateTime? bookingDate { get; set; }
    }
}
