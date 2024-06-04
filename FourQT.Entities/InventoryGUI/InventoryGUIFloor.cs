using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.InventoryGUI
{
    public class InventoryGUIFloor : InventoryGUIFloorCore
    {
        public int floorId { get; set; }
        public string? floorName { get; set; }
        public string? unitAreaRange { get; set; }
        public string? unitTypeGroups { get; set; }
        public int availableUnits { get; set; }
        public int bookedUnits { get; set; }
        public int mortgageUnits { get; set; }
        public int holdUnits { get; set; }
        public int soldUnits { get; set; }
        public int totalUnits { get; set; }
        public string? status { get; set; }
    }

    public class InventoryGUIFloorAlt : InventoryGUIFloorCore,InventoryGUITowerCoreI
    {       
        public int towerId { get; set; }
        public string? towerName { get; set; }
        public int floorId { get; set; }
        public string? floorName { get; set; }
    }

    public interface InventoryGUIFloorCore
    {
        public int floorId { get; set; }
        public string? floorName { get; set; }
    }

    
}
