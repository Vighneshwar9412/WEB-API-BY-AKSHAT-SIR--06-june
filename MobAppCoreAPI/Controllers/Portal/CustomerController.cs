using Microsoft.AspNetCore.Mvc;
using FourQT.Entities.Portal;
using FourQT.Entities.Portal.Referrals;
using MobAppCoreAPI.Interfaces.Portal;
using Microsoft.EntityFrameworkCore;

namespace MobAppCoreAPI.Controllers.Portal
{
    [Route("/api/p1/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "p1")]
    public class CustomerController : ControllerBase
    {
        private readonly IPortalCustomer _IPortalCustomer;

        public CustomerController(IPortalCustomer IPortalCustomer)
        {
            _IPortalCustomer = IPortalCustomer;
        }
       
        //[HttpPost]
        //public ResponseStatus<HomePages> GetPortal_HomePages(CustomerLogin objuser)
        //{
        //    return _IPortalCustomer.getPortal_HomePages(objuser);
        //}     

        //[HttpPost]
        //public ResponseStatus<CustomerLoginList> GetCustomerUnitNumber(CustomerLogin objuser)
        //{
        //    return _IPortalCustomer.getCustomerUnitNumber(objuser);
        //}

        //[HttpPost]
        //public ResponseStatus<CustomerReferral> GetCustomerReferralList(CustomerReferral Obj)
        //{
        //    return _IPortalCustomer.getCustomerReferralList(Obj);
        //}

        //[HttpPost]
        //public ResponseStatus<Getquery> Getudp_getquery(CustomerDemand objuser)
        //{
        //    return _IPortalCustomer.getudp_getquery(objuser);
        //}

        //[HttpPost]
        //public ResponseStatus<InsertqueryStatus> Getudp_InsertqueryHistory(InsertqueryHistory ObjInsertqueryHistory)
        //{
        //    return _IPortalCustomer.getudp_InsertqueryHistory(ObjInsertqueryHistory);
        //}

        //[HttpPost]
        //public ResponseStatus<FAQList> GetFAQ(CustomerLoginList objuser)
        //{
        //    return _IPortalCustomer.getFAQ(objuser);
        //}

        //[HttpPost]
        //public ResponseStatus<NoData> SaveCustomerReferral(CustomerReferral Obj)
        //{
        //    return _IPortalCustomer.saveCustomerReferral(Obj);
        //}

        //[HttpPost]
        //public ResponseStatus<ConstructionUpdate> GetConstruction(CustomerLoginList objuser)
        //{
        //    return _IPortalCustomer.getConstruction(objuser);
        //}

        //[HttpPost]
        //public ResponseStatus<Notification> GetNotification(CustomerLoginList objuser)
        //{
        //    return _IPortalCustomer.getNotification(objuser);
        //}



        //[HttpPost]
        //public ResponseStatus<CustomerNameMobile> GetCustomerNameMobile(CustomerDemand objuser)
        //{
        //    return _IPortalCustomer.getCustomerNameMobile(objuser);
        //}

    }
}
