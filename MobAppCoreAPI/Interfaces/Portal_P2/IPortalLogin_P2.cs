using FourQT.Entities.Portal;

namespace MobAppCoreAPI.Interfaces.Portal_P2
{
    public interface IPortalLogin_P2
    {       
        public Task<ResponseStatusToken_New<Customeruserlist>> customerLogin(CustomerLoginShort objuser);
        public Task<ResponseStatus_New<ChangePasswordList>> customerChangePassword(ChangePasswordNew objuser,HttpRequest req);
        public Task<ResponseStatus_New<NoData>> getOTP(CustomerLogin objuser,HttpRequest req);
        public Task<ResponseStatus_New<InsertqueryStatus>> CustomerLogOut(CustomerCore1 objuser, HttpRequest req);

    }
}
