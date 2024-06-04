using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.InventoryGUI
{
    public class InventoryGUIRequest
    {
        public string? token { get; set; }
        public int stage { get; set; }
        public int projectId { get; set; }
        public int towerId { get; set; }
        public int floorId { get; set; }
        public int unitId { get; set; }
    }
}
