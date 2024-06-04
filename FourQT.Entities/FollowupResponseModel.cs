using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class FollowUpList : iLeadImp
    {
        public string? response { get; set; }
        public string? subResponse { get; set; }
        public string? displayFollowedDate { get; set; }
        public string? followedBy { get; set; }
        public string? displayNextFollowDate { get; set; }
        public string? meetingDate { get; set; }
        public string? projectName { get; set; }
        public string? unitType { get; set; }
        public string? remarks { get; set; }
        public int sourceId { get; set; }
        public string? enquiryType { get; set; }
        public string? callDirection { get; set; }
        public string? displayEnquiryDate { get; set; }
        public string? displayStatus { get; set; }
        public string? displayName { get; set; }
        public string? recordingUrl { get; set; }
        public string? displaySubResponse { get; set; }
        public string? source { get; set; }
        public string? meetingDuration { get; set; }
    }

    public class FollowupResponseModel
    {
        public List<FollowUpList> FollowupList { get; set; } = new List<FollowUpList>();

    }

    public class FollowupNotificationWrap
    {
        public List<FollowupNotification>? followupNotifications { get; set; } = new List<FollowupNotification>();
    }

    public class FollowupNotification
    {
        public int sno {  get; set; }
        public string? mobile { get; set; }
        public string? time { get; set; }
        public string? title { get; set; }
        public string? subtitle { get; set; }
        public string? description { get; set; }
    }
}
