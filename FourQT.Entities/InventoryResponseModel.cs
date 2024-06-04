using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class Inventory
    {
        public string ProjectName { get; set; }
        public string TowerName { get; set; }

        public string FloorName { get; set; }

        public string GroupName { get; set; }

        public string UnitType { get; set; }

        public string UnitNo { get; set; }

        public string UnitLocation { get; set; }

        public string Status { get; set; }

        public string HoldBy { get; set; }  

        public string HoldDate { get; set; }

        public string SuperArea { get; set; }

        public string CarpetArea { get; set; }

        public string BuildUpArea { get; set; }

        

    }

    public class InventoryResponseModel
    {
        public List<Inventory> InventoryList { get; set; } = new List<Inventory>();
    }
}
