using FourQT.Entities.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobAppCoreAPI.Interfaces.General;
using Swashbuckle.AspNetCore.Annotations;

namespace MobAppCoreAPI.Controllers.General
{
    [Route("/api/g1/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "g1")]
    public class MiscellaneousController : ControllerBase
    {
        private readonly IMiscellaneous _IMiscellaneous;

        public MiscellaneousController(IMiscellaneous IMiscellaneous)
        {
            _IMiscellaneous = IMiscellaneous;
        }      

        [HttpPost]
        public async Task<dynamic> GenerateQRCode(QRCodeRequest model)
        {
            return await _IMiscellaneous.generateQRCode(model, HttpContext);
        }
    }
}
