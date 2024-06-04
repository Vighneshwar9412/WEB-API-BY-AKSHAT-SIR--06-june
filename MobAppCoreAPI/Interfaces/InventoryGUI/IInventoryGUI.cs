using FourQT.Entities.InventoryGUI;

namespace MobAppCoreAPI.Interfaces.InventoryGUI
{
    public interface IInventoryGUI
    {
        public Task<dynamic> getInventoryGUI(InventoryGUIRequest model);
    }
}
