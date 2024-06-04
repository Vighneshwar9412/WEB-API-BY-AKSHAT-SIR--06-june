using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.ChannelPartner
{
    public class RegisterLeadRequest : RegisterLeadRequestCore
    {
        public string? salutation { get; set; }
        public string? customerName { get; set; }
        public string? customerMobile { get; set; }
        public string? customerEmail { get; set; }
        public string? customerRemarks { get; set; }
        public int projectId { get; set; }
        public string? cpEmployeeName { get; set; }
        public string? cpEmployeeMobile { get; set; }
        public string? ipAddress { get; set; }
        public string? sendOtpForLeadRegister { get; set; }
        public string? uploadCustomerPhoto { get; set; }
        public string? customerPhoto { get; set; }
        public string? customerPhotoFormat { get; set; }
        public string? sendWhatsAppForLeadRegister { get; set; }
        public int salesEmpId { get; set; }
    }

	public class RegisterLeadResponse
	{
		public int leadRegStatus { get; set; }
		public string? outMsg { get; set; }
	}

	public class RegisterLeadRequest_2 : RegisterLeadRequestCore, RegisterLeadRequestOtp
	{
        public string? customerMobile { get; set; }
        public string? otp { get; set; }
    }

	public interface RegisterLeadRequestCore
	{
        public string? customerMobile { get; set; }
    }

    public interface RegisterLeadRequestOtp
	{
        public string? otp { get; set; }
    }

	public class CPLead
	{
        public int enquiryId { get; set; }
        public string? enquiryDate { get; set; }
        public string? projectName { get; set; }
        public string? source { get; set; }
        public string? customerName { get; set; }
        public string? customerMobile { get; set; }
        public string? customerEmail { get; set; }
        public string? customerRemarks { get; set; }
        public string? cpEmployeeName { get; set; }
        public string? cpEmployeeMobile { get; set; }
        public string? salesEmployeeName { get; set; }
        public string? salesEmployeeMobile { get; set; }             
    }

    public class LeadListRequest
    {
        public string? searchText { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public int pageNo { get; set; }
        public int pageSize { get; set; }
    }
    public class CPLeadListing
    {
        public int totalRecords { get; set; }
        public List<CPLead>? lead { get; set; } = new List<CPLead>();
    }
}
