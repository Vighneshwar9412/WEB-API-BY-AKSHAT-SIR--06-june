using FourQT.Entities;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/save-location")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class SaveSVLocationController : Controller
    {

        private readonly ISaveSVLocation _Isavesvlocation;
        public SaveSVLocationController(ISaveSVLocation savelocation)
        {

            _Isavesvlocation = savelocation;

        }

        [HttpPost]

        public async Task<APIObjectResponse> SaveEnquiry([FromBody] SVLocationShort model)
        {
            return await _Isavesvlocation.SVlocationsave(Request, model,HttpContext);

        }
    }
}
