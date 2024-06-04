using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.InventoryGUI
{
    public class InventoryGUITower : InventoryGUITowerCoreI
    {
        public int towerId { get; set; }
        public string? towerName { get; set; }
        public string? unitAreaRange { get; set; }
        public string? unitTypeGroups { get; set; }
        public int availableUnits { get; set; }
        public int bookedUnits { get; set; }
        public int mortgageUnits { get; set; }
        public int holdUnits { get; set; }
        public int soldUnits { get; set; }
        public int totalUnits { get; set; }
    }

    public class InventoryGUITowerCore : InventoryGUITowerCoreI
    {
        public int towerId { get; set; }
        public string? towerName { get; set; }
    }

    public interface InventoryGUITowerCoreI
    {
        public int towerId { get; set; }
        public string? towerName { get; set; }       
    }
}
