using Microsoft.AspNetCore.Mvc;
using FourQT.Entities.Firebase;
using FourQT.Entities.Portal;
using MobAppCoreAPI.Interfaces.Portal_P2;
using MobAppCoreAPI.Attributes.Portal_P2;

namespace MobAppAPI.Controllers
{
    [Route("/api/p2/[controller]/[action]")]
    [ApiController]
    [APIPortalKey]
    [ApiExplorerSettings(GroupName = "p2")]
    public class FirebaseController : ControllerBase
    {
        private readonly IPortalFirebase _IPortalFirebase;

        public FirebaseController(IPortalFirebase IPortalFirebase)
        {
            _IPortalFirebase = IPortalFirebase;
        }

        #region Firebase Notification

        [HttpPost]
        public ResponseStatus<Notification_Device> Notification(Firebase objFirebase)
        {
            return _IPortalFirebase.Notification(objFirebase,Request);

        }
        #endregion
    }
}
