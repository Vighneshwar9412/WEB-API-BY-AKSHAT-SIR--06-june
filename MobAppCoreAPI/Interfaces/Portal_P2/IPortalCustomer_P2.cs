using FourQT.Entities;
using FourQT.Entities.Portal;
using FourQT.Entities.Portal.Referrals;

namespace MobAppCoreAPI.Interfaces.Portal_P2
{
    public interface IPortalCustomer_P2
    {
        public Task<ResponseStatus_New<HomePagesNew>> getPortal_HomePages(CustomerCore1 objuser,HttpRequest request, HttpContext context);      
        public Task<ResponseStatus_New<CustomerLoginListNew>> getCustomerUnitNumber(CustomerCore1 objuser, HttpRequest request, HttpContext context);
        public Task<ResponseStatus_New<CustomerReferral>> getCustomerReferralList(CustomerReferral Obj, HttpRequest request);
        public Task<ResponseStatus_New<Getquery>> getudp_getquery(HttpRequest request);
        public Task<ResponseStatus_New<InsertqueryStatus>> getudp_InsertqueryHistory(InsertqueryHistoryNew ObjInsertqueryHistory, HttpRequest request, HttpContext context);
        public Task<ResponseStatus_New<FAQList>> getFAQ(HttpRequest request);
        public Task<ResponseStatus_New<NoData>> saveCustomerReferral(CustomerReferral Obj, HttpRequest request);
        public Task<ResponseStatus_New<ConstructionUpdate>> getConstruction(CustomerCore2 objuser, HttpRequest request);
        public Task<ResponseStatus_New<Notification>> getNotification(HttpRequest request);
        public Task<ResponseStatus_New<CustomerNameMobile>> getCustomerNameMobile(CustomerCore4 objuser, HttpRequest request);



        public Task<ResponseStatus_New<CustomerDetail>> getCustomerDetails(CustomerCore2 objuser,HttpRequest request);
        public Task<ResponseStatus_New<MultipleApplicant>> getUdp_MultipleApplicant(CustomerCore2 objuser, HttpRequest request);
        public Task<ResponseStatus_New<UnitDetailList>> UnitDetails(CustomerCore2 objuser, HttpRequest request);
        public Task<ResponseStatus_New<AccountDeatailList>> getAccount(CustomerCore2 objuser, HttpRequest request);
        public Task<ResponseStatusPayment_New<PaymentscheduleListJsion>> getOutstandingDetail_Paymentschedule(CustomerCore5 objuser, HttpRequest request);
        public Task<ResponseStatusRecipt_New<ReceiptDetailsDataList>> getReceiptDetails(CustomerCore2 objuser, HttpRequest request);
        public Task<ResponseStatus_New<LetterDetailCustomer>> getLetterDetailCustomerPortal(CustomerCore2_2 objuser, HttpRequest request);
        public Task<ResponseStatus_New<ContactUS>> getContactUS(HttpRequest request);
        public Task<ResponseStatus_New<ViewProjectDocuments>> GetProjectDocuments(CustomerCore2 objuser, HttpRequest request);
        public Task<ResponseStatus_New<ViewCustomerDocument>> GetCustomerDocuments(CustomerDoc objuser, HttpRequest request);
        public Task<ResponseStatus_New<CustomerDuesPaid>> getCustomerDuesPaid(CustomerCore2_2 ObjCustomer, HttpRequest request); 
        public Task<ResponseStatus_New<NoData>> saveCustomerReferral_P2(CustomerReferralRequest ObjCustomer, HttpRequest request,HttpContext context);
        public Task<ResponseStatus_New<CustomerReferralDetails>> getCustomerReferralReport(CustomerCore1 ObjCustomer, HttpRequest request, HttpContext context);
        public Task<ResponseStatus_New<AccountDetail_New_Wrap>> getAccount_New(CustomerCore2 objuser, HttpRequest request);
        public Task<ResponseStatus_New<PortalExtraInfo_Wrap>> getPortalExtraInfo(HttpRequest request); 
        public Task<ResponseStatus_New<dynamic>> getRaisedQueryDetails(QueryDetailsRequest objRequest, HttpRequest request);
        public Task<dynamic> GetProjectDocumentsByType(CustomerCore5 objRequest, HttpRequest request);
    }

}
