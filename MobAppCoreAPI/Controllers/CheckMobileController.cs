using FourQT.Entities;
using FourQT.UserRights;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Controllers
{
    [Route("/api/v1/check-duplicity")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CheckMobileController : ControllerBase
    {
        //private readonly ICheckMobile _checkmobile;

        //public CheckMobileController(ICheckMobile checkmobile)
        //{
           // _checkmobile = checkmobile;
        //}

        [HttpGet]
        public async Task<APIObjectResponse> checkduplicity(string mobileNo)
        {
            // return await _loginInterface.LoginAsync(model, mkey);
            return await (new UserRightsBLL()).checkduplicity(Request, mobileNo);
        }
    }
}
