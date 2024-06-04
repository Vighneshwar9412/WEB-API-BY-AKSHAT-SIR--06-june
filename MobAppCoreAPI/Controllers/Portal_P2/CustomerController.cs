using Microsoft.AspNetCore.Mvc;
using FourQT.Entities.Portal;
using FourQT.Entities.Portal.Referrals;
using MobAppCoreAPI.Interfaces.Portal_P2;
using MobAppCoreAPI.Attributes.Portal_P2;
using FourQT.CommonFunctions.Portal;
using FourQT.Entities;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using FourQT.DAL.Portal;
using Swashbuckle.AspNetCore.Annotations;

namespace MobAppCoreAPI.Controllers.Portal_P2
{
    [Route("/api/p2/[controller]/[action]")]
    [ApiController]
    [APIPortalKey]
    [ApiExplorerSettings(GroupName = "p2")]
    public class CustomerController : ControllerBase
    {
        private readonly IPortalCustomer_P2 _IPortalCustomer;

        public CustomerController(IPortalCustomer_P2 IPortalCustomer)
        {
            _IPortalCustomer = IPortalCustomer;
        }

        [HttpPost]
        [SwaggerOperation(Description = "Customer Portal login\r\n( Token : eKey from ValidateKey,\r\n   DeviceId : device fcm token,\r\n   fromDevice : 1 (Android) / 2 (IOS)\r\n)")]
        
        public async Task<ResponseStatus_New<HomePagesNew>> GetPortal_HomePages(CustomerCore1 objuser)
        {
            return await _IPortalCustomer.getPortal_HomePages(objuser,Request,HttpContext);
        }     

        [HttpPost]
        public async Task<ResponseStatus_New<CustomerLoginListNew>> GetCustomerUnitNumber(CustomerCore1 objuser)
        {
            return await _IPortalCustomer.getCustomerUnitNumber(objuser, Request, HttpContext);
        }

        //[HttpPost]
        //public ResponseStatus<CustomerReferral> GetCustomerReferralList(CustomerReferral Obj)
        //{
        //    return _IPortalCustomer.getCustomerReferralList(Obj, Request);
        //}

        [HttpPost]
        public async Task<ResponseStatus_New<Getquery>> Getudp_getquery()
        {
            return await _IPortalCustomer.getudp_getquery(Request);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<InsertqueryStatus>> Getudp_InsertqueryHistory(InsertqueryHistoryNew ObjInsertqueryHistory)
        {
            return await _IPortalCustomer.getudp_InsertqueryHistory(ObjInsertqueryHistory, Request, HttpContext);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<FAQList>> GetFAQ()
        {
            return await _IPortalCustomer.getFAQ(Request);
        }

        //[HttpPost]
        //public ResponseStatus<NoData> SaveCustomerReferral(CustomerReferral Obj)
        //{
        //    return _IPortalCustomer.saveCustomerReferral(Obj, Request);
        //}

        [HttpPost]
        public async Task<ResponseStatus_New<ConstructionUpdate>> GetConstruction(CustomerCore2 objuser)
        {
            return await _IPortalCustomer.getConstruction(objuser, Request);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<Notification>> GetNotification()
        {
            return await _IPortalCustomer.getNotification(Request);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<CustomerNameMobile>> GetCustomerNameMobile(CustomerCore4 objuser)
        {
            return await _IPortalCustomer.getCustomerNameMobile(objuser, Request);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<CustomerDetail>> GetCustomerDetails(CustomerCore2 objuser)
        {
            return await _IPortalCustomer.getCustomerDetails(objuser, Request);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<MultipleApplicant>> GetUdp_MultipleApplicant(CustomerCore2 objuser)
        {
            return await _IPortalCustomer.getUdp_MultipleApplicant(objuser, Request);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<UnitDetailList>> UnitDetails(CustomerCore2 objuser)
        {
            return await _IPortalCustomer.UnitDetails(objuser, Request);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<AccountDeatailList>> GetAccount(CustomerCore2 objuser)
        {
            return await _IPortalCustomer.getAccount(objuser, Request);
        }

        [HttpPost]
        public async Task<ResponseStatusPayment_New<PaymentscheduleListJsion>> GetOutstandingDetail_Paymentschedule(CustomerCore5 objuser)
        {
            return await _IPortalCustomer.getOutstandingDetail_Paymentschedule(objuser, Request);
        }

        [HttpPost]
        public async Task<ResponseStatusRecipt_New<ReceiptDetailsDataList>> GetReceiptDetails(CustomerCore2 objuser)
        {
            return await _IPortalCustomer.getReceiptDetails(objuser, Request);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<LetterDetailCustomer>> GetLetterDetailCustomerPortal(CustomerCore2_2 objuser)
        {
            return await _IPortalCustomer.getLetterDetailCustomerPortal(objuser, Request);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<ContactUS>> GetContactUS()
        {
            return await _IPortalCustomer.getContactUS(Request);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<ViewProjectDocuments>> GetProjectDocuments(CustomerCore2 objuser)
        {
            return await _IPortalCustomer.GetProjectDocuments(objuser,Request);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<ViewCustomerDocument>> GetCustomerDocuments(CustomerDoc objuser)
        {
            return await _IPortalCustomer.GetCustomerDocuments(objuser, Request);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<CustomerDuesPaid>> GetCustomerDuesPaid(CustomerCore2_2 ObjCustomer)
        {
            return await _IPortalCustomer.getCustomerDuesPaid(ObjCustomer, Request);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<NoData>> SaveCustomerReferral(CustomerReferralRequest ObjCustomer)
        {
            return await _IPortalCustomer.saveCustomerReferral_P2(ObjCustomer, Request, HttpContext);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<CustomerReferralDetails>> GetCustomerReferralReport(CustomerCore1 ObjCustomer)
        {
            return await _IPortalCustomer.getCustomerReferralReport(ObjCustomer, Request, HttpContext);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<AccountDetail_New_Wrap>> GetAccount_New(CustomerCore2 objuser)
        {
            return await _IPortalCustomer.getAccount_New(objuser, Request);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<PortalExtraInfo_Wrap>> GetPortalExtraInfo()
        {
            return await _IPortalCustomer.getPortalExtraInfo(Request);
        }

        [HttpPost]
        public async Task<ResponseStatus_New<dynamic>> GetRaisedQueryDetails(QueryDetailsRequest objRequest)
        {
            return await _IPortalCustomer.getRaisedQueryDetails(objRequest, Request);
        }

        [HttpPost]
        public async Task<dynamic> GetProjectDocumentsByType(CustomerCore5 objuser)
        {
            return await _IPortalCustomer.GetProjectDocumentsByType(objuser, Request);
        }
    }
}
