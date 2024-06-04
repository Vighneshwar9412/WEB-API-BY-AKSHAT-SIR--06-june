using FourQT.Entities.General;
using MobAppCoreAPI.Interfaces.General;
using System.Net;
using Newtonsoft.Json;
using FourQT.Core.General;

namespace MobAppCoreAPI.Repository.General
{
    public class MiscellaneousRepository : IMiscellaneous
    {
        public async Task<dynamic> generateQRCode(QRCodeRequest model, HttpContext context)
        {
            return await (new MiscellaneousOperationsBLL()).GenerateQRCode(model, context);
        }
    }
}
