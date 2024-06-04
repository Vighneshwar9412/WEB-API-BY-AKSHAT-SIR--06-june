using FourQT.Entities.Portal;

namespace MobAppCoreAPI.Interfaces.Portal
{
    public interface IPortalLogin
    {
        public ResponseStatus<Customeruserlist> customerLogin(CustomerLogin objuser);
        public ResponseStatus<ChangePasswordList> customerChangePassword(ChangePassword objuser);
        public ResponseStatus<NoData> getOTP(CustomerLogin objuser);

    }
}
