using FourQT.Entities.Portal;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Interfaces.General;

namespace MobAppCoreAPI.Controllers.General
{
    [Route("/api/g1/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "g1")]
    public class ValidateKeyController : ControllerBase
    {
        private readonly IValidateKey _IValidateKey;

        public ValidateKeyController(IValidateKey IValidateKey)
        {
            _IValidateKey = IValidateKey;
        }

        [HttpGet]
        public ResponseStatus<ValidateKey> ValidateKey(string mKey)
        {
            return _IValidateKey.validateKey(mKey);
        }       

    }
}
