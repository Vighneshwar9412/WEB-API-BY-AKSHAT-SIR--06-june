using FourQT.Entities.Firebase;
using FourQT.Entities.Portal;

namespace MobAppCoreAPI.Interfaces.Portal_P2
{
    public interface IPortalFirebase
    {
        public ResponseStatus<Notification_Device> Notification(Firebase objFirebase,HttpRequest request);
    }
}
