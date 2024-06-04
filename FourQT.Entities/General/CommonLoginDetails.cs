namespace FourQT.Entities.General
{
    public interface ICommonLoginDetails
    {
        public string? token { get; set; }
        public int id { get; set; }
        public string? commonLoginType { get; set; }
        public string? name { get; set; }
        public string? photo { get; set; }
        public string? mobileNo { get; set; }
        public string? email { get; set; }
        public string? companyName { get; set; }
        public string? companyLogo { get; set; }
        public string? type { get; set; }
        public int status { get; set; }
        public string? sendOtpForLeadRegister { get; set; }
        public string? uploadCustomerPhoto { get; set; }
        public string? sendWhatsAppForLeadRegister { get; set; }
        public string? qrCode { get; set; }
        public int hRuserId { get; set; }
        public bool attendenceModule { get; set; }
        public bool enableInventory { get; set; }
        public bool enableInventoryOperationsCP { get; set; }

    }

    public class CustomerPortalCommonLoginResponse : ICommonLoginDetails
    {
        public string? token { get; set; }
        public string? commonLoginType { get; set; }
        public int id { get; set; }
        public string? name { get; set; }
        public string? photo { get; set; }
        public string? mobileNo { get; set; }
        public string? email { get; set; }
        public string? companyName { get; set; }
        public string? companyLogo { get; set; }
        public string? type { get; set; }
        public int status { get; set; }
        public string? sendOtpForLeadRegister { get; set; }
        public string? uploadCustomerPhoto { get; set; }
        public string? sendWhatsAppForLeadRegister { get; set; }
        public string? qrCode { get; set; }
        public int hRuserId { get; set; }
        public bool attendenceModule { get; set; }
        public bool enableInventory { get; set; }
        public bool enableInventoryOperationsCP { get; set; }
    }

    public class EmployeeCommonLoginResponse : ICommonLoginDetails
    {
        public string? token { get; set; }
        public string? commonLoginType { get; set; }
        public int id { get; set; }
        public string? name { get; set; }
        public string? photo { get; set; }       
        public string? mobileNo { get; set; }
        public string? email { get; set; }
        public string? companyName { get; set; }
        public string? companyLogo { get; set; }
        public string? type { get; set; }
        public int status { get; set; }
        public string? sendOtpForLeadRegister { get; set; }
        public string? uploadCustomerPhoto { get; set; }
        public string? sendWhatsAppForLeadRegister { get; set; }
        public string? qrCode { get; set; }
        public int hRuserId { get; set; }
        public bool attendenceModule { get; set; }
        public bool enableInventory { get; set; }
        public bool enableInventoryOperationsCP { get; set; }
    }

    public class ChannelPartnerCommonLoginResponse : ICommonLoginDetails
    {
        public string? token { get; set; }
        public string? commonLoginType { get; set; }
        public int id { get; set; }
        public string? name { get; set; }
        public string? photo { get; set; }
        public string? mobileNo { get; set; }
        public string? email { get; set; }
        public string? companyName { get; set; }
        public string? companyLogo { get; set; }
        public string? type { get; set; }
        public int status { get; set; }
        public string? sendOtpForLeadRegister { get; set; }
        public string? uploadCustomerPhoto { get; set; }
        public string? sendWhatsAppForLeadRegister { get; set; }
        public string? qrCode { get; set; }
        public int hRuserId { get; set; }
        public bool attendenceModule { get; set; }
        public bool enableInventory { get; set; }
        public bool enableInventoryOperationsCP { get; set; }
    }
}
