using FourQT.Entities.HR;
using FourQT.Entities.Portal;

namespace MobAppCoreAPI.Interfaces.Portal_P2
{
    public interface IPortalHR
    {
        public List<AttendanceResponseModel> HRAttendanceLogin(AttendanceRequestModel objAttendanceRequest,HttpRequest request);

    }
}
