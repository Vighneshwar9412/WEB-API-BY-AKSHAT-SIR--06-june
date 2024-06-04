using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.InventoryGUI
{
    public class InventoryGUIStages
    {
    }

    public class InventoryGUIStage_1
    {
        public InventoryGUIProject? projectDetails { get; set; } = new InventoryGUIProject();
        public List<InventoryGUITower>? towerList { get; set; } = new List<InventoryGUITower>();
    }

    public class InventoryGUIStage_2
    {
        public InventoryGUITowerCore? towerDetails { get; set; } = new InventoryGUITowerCore();
        public List<InventoryGUIFloor>? floorList { get; set; } = new List<InventoryGUIFloor>();
    }

    public class InventoryGUIStage_3
    {
        public InventoryGUIFloorAlt? floorDetails { get; set; } = new InventoryGUIFloorAlt();
        public List<InventoryGUIUnit>? unitList { get; set; } = new List<InventoryGUIUnit>();
    }
}
