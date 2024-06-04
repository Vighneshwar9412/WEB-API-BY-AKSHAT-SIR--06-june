using FourQT.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/project-docs")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ProjectDocsController : ControllerBase
    {
        private readonly IProjectDocs _IProjectDocs;

        public ProjectDocsController(IProjectDocs IProjectdocs)
        {
            _IProjectDocs = IProjectdocs;
        }

        [HttpGet]
        public async Task<dynamic> projectdocslisting(int projectId,int itemId)
        {
            return await _IProjectDocs.projectdocslisting(Request, projectId, itemId);
        }

    }


}
