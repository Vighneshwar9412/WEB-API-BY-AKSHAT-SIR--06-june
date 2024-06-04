using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.InventoryGUI
{
    public class InventoryGUIProject
    {
        public int projectId { get; set; }
        public string? projectName { get; set; }
        public string? projectAddress { get; set; }
        public string? projectArea { get; set; }
        public string? areaUnit { get; set; }
        public string? description { get; set; }
        public int towers { get; set; }
        public int floors { get; set; }
        public int units { get; set; }
    }
}
