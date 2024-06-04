using FourQT.Entities;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Controllers
{

    [Route("api/v1/update-customer")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UpdateCustomerController : ControllerBase
    {
        private readonly IUpdateCustomer _Iupdatecustomer;

        public UpdateCustomerController(IUpdateCustomer updatecustomer)
        {
            _Iupdatecustomer = updatecustomer;
        }

        [HttpPost]

        public async Task<APIObjectResponse> UpdateCustomer([FromBody] PartyUpdate model)
        {
            return await _Iupdatecustomer.updatecustomer(Request, model,HttpContext);

        }
    }
}
