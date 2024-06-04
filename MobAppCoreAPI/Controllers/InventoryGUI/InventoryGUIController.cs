using FourQT.Entities.InventoryGUI;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Interfaces.InventoryGUI;
using MobAppCoreAPI.Repository.InventoryGUI;
using Swashbuckle.AspNetCore.Annotations;

namespace MobAppCoreAPI.Controllers.InventoryGUI
{
    [Route("/api/ig1/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ig1")]
    public class InventoryGUIController : ControllerBase
    {
        private readonly IInventoryGUI _IInventoryGUI;

        public InventoryGUIController(IInventoryGUI IInventoryGUI)
        {
            _IInventoryGUI = IInventoryGUI;
        }

        [HttpPost]
        [SwaggerOperation(Description = "Inventory GUI \r\n(Stage: '1' Initial Project View, '2' Tower Click, '3' Floor Click, '4' Unit Click)")]
        public async Task<dynamic> GetInventoryGUI([FromBody] InventoryGUIRequest model)
        {
            return await _IInventoryGUI.getInventoryGUI(model);
        }

    }
}
