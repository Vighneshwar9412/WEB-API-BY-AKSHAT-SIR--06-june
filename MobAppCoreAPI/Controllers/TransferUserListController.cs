using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/transfer-user-list")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class TransferUserListController : ControllerBase
    {
        private readonly ITransferUserList   _Itransferuser;

        public TransferUserListController(ITransferUserList Itransferuser)
        {
            _Itransferuser = Itransferuser;
        }

        [HttpGet]
        public async Task<dynamic> transferuserlisr()
        {
            return await _Itransferuser.transferuserlist(Request);
        }
    }
}
