using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MobAppCoreAPI.Interfaces;
using MobAppCoreAPI.Attributes;
using FourQT.Entities;

namespace MobAppCoreAPI.Controllers
{   
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ListAllEnquiryMastersAPIController : ControllerBase
    {
        private readonly IListAllEnquiryMasters _IListAllEnquiryMasters;
        public ListAllEnquiryMastersAPIController(IListAllEnquiryMasters IListAllEnquiryMasters)
        {
            _IListAllEnquiryMasters = IListAllEnquiryMasters;
        }

        [HttpGet]
        [Route("api/v1/list-enquiry-masters")]
        public async Task<dynamic> listAllEnquiryMasters(int typeId)
        {
            return await _IListAllEnquiryMasters.listAllEnquiryMasters(Request,typeId);
        }

        [HttpPost]
        [Route("api/v1/EnquiryMastersByEnqId")]
        public async Task<dynamic> EnquiryMastersByEnqId([FromBody] MastersByEnquiryReq model)
        {
            return await _IListAllEnquiryMasters.enquiryMastersByEnqId(Request, model);
        }
    }
}
