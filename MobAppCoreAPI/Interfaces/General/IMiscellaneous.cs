using FourQT.Entities.General;

namespace MobAppCoreAPI.Interfaces.General
{
    public interface IMiscellaneous
    {        
        public Task<dynamic> generateQRCode(QRCodeRequest model,HttpContext context);
    }
}
