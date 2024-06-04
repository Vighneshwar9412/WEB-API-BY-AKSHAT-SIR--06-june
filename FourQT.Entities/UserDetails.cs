using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class UserDetails
    {
        public int login_Id { get; set; }
         public string? emp_Name { get; set; }
        public string? UserPhoto { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public bool ClickToCallRight { get; set; }
        public bool WhatsAppRight { get; set; }
        public string? WhatsAppAPI { get; set; }
        public bool SourceDisplayRight { get; set; }
        public bool MobileDisplayRight { get; set; }
        public bool GeoLocationForSiteVisit { get; set; }
        public bool DumpRight { get; set; }
        public bool TransferRight { get; set; }
        public bool SuccessRight { get; set; }
        public bool EmailRight { get; set; }
        public bool SMSRight { get; set; }
        public string? LoginType { get; set; }   
        public int channelId { get; set; }
        public int docNo { get; set; }
        public string? callerId { get; set; }
        public string? agentNumber { get; set; }
        public string? transferType { get; set; }
        public string? transferAutoManual { get; set; }
        public int hRuserId { get; set; }
        public bool attendenceModule { get; set; }
        public bool leadInventory { get; set; }
    }

    public class BrokerDetails
    { 
        public int Id { get; set; }
        
        public int LoginId { get; set; }
        
        public string? CompanyName { get; set;}

        public int ParentBrokerId    { get; set; }

        public int EmployeeId { get; set;}
        
    }

    public class UserDetailsCP
    {
        public int loginId { get; set; }
        public string? empName { get; set; }
        public string? UserPhoto { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public bool ClickToCallRight { get; set; }
        public bool WhatsAppRight { get; set; }
        public string? WhatsAppAPI { get; set; }
        public bool SourceDisplayRight { get; set; }
        public bool MobileDisplayRight { get; set; }
        public bool GeoLocationForSiteVisit { get; set; }
        public bool DumpRight { get; set; }
        public bool TransferRight { get; set; }
        public bool SuccessRight { get; set; }
        public bool EmailRight { get; set; }
        public bool SMSRight { get; set; }
        public string? LoginType { get; set; }
        public int channelId { get; set; }
        public int docNo { get; set; }
        public string? callerId { get; set; }
        public string? agentNumber { get; set; }
        public string? transferType { get; set; }
        public string? transferAutoManual { get; set; }
        public string? sendOtpForLeadRegister { get; set; }
    }
}
