using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Construction
{
    public class HRAttendenceEmpDetails {
        public int hRuserId { get; set; }
        public string? employeeName { get; set; }
        public string? employeeCode { get; set; }
        public string? employeeDept { get; set; }
        public string? employeeDesignation { get; set; }
        public string? employeeAddress { get; set; }
        public string? currentDateTime { get; set; }
        public string? batchTiming { get; set; }
        public string? totalTime { get; set; }

    }

    public class HRAttendenceLoginDetails {
        public Boolean loginEnabled { get; set; }
        public Boolean logoutEnabled { get; set; }
        public Boolean showLoginData { get; set; }
        public Boolean showLogoutData { get; set; }
        public string? loginTime { get; set; }
        public string? logoutTime { get; set; }
        public string? loginLocation { get; set; }
        public string? logoutLocation { get; set; }
        public string? loginRemarks { get; set; }
        public string? logoutRemarks { get; set; }
        public string? loginPhoto { get; set; }
        public string? logoutPhoto { get; set; }
        public string? workingTimeRequired { get; set; }
        public string? workingTimeCompleted { get; set; }
        public string? logDate { get; set; }
    }

    public class HRAttendence
    {
        public HRAttendenceEmpDetails? empDetails { get; set; } = new HRAttendenceEmpDetails();
        public HRAttendenceLoginDetails? loginDetails { get; set; } = new HRAttendenceLoginDetails();
    }

    public class HRAttendenceRequest {
        public int hRuserId { get; set; }
        public string? loginAction { get; set; }
        public string? remarks { get; set; }
        public string? location { get; set; }
        public string? photo { get; set; }
        public string? photoFormat { get; set; }
    }

    public class HRAttendenceReportRequest: HRAttendenceReportRequestCore
    {
        public int month { get; set; }
        public int year { get; set; }
    }

    public class HRAttendenceReportRequestCore {
        public int hRuserId { get; set; }
    }

    public class HRAttendenceReport
    {
        public int hRuserId { get; set; }
        public string? employeeName { get; set; }
        public string? loginDate { get; set; }
        public string? loginTime { get; set; }
        public string? logoutTime { get; set; }
        public string? loginLocation { get; set; }
        public string? logoutLocation { get; set; }
        public string? loginRemarks { get; set; }
        public string? logoutRemarks { get; set; }
        public string? loginPhoto { get; set; }
        public string? logoutPhoto { get; set; }
        public string? workingTimeRequired { get; set; }
        public string? workingTimeCompleted { get; set; }
    }

    public class HRAttendenceReportWrap
    {    
        public List<HRAttendenceReport>? attendenceList { get; set; }

    }

    public class HRAttendenceReportMonth 
    { 
        public int month { get; set; }
        public int year { get; set; }
        public string? displayMonth { get; set; }

    }
}
