using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FourQT.Entities
{
    [Serializable]
    public class LeadInventoryUnit
    {
        [XmlElement("UnitId")]
        public int unitId { get; set; }

        //[XmlElement("Project")]
        //public string? projectId { get; set; }

        //[XmlElement("Tower")]
        //public string? towerId { get; set; }

        //[XmlElement("Floor")]
        //public string? floorId { get; set; }

        [XmlElement("UnitNo")]
        public string? unitNo { get; set; }

        [XmlElement("Status")]
        public string? status { get; set; }

        [XmlElement("ColorCode")]
        public string? colorCode { get; set; }      

    }

    [Serializable]
    public class LeadInventoryFloor 
    {
        [XmlElement("FloorId")]
        public int floorId { get; set; }

        [XmlElement("FloorName")]
        public string? floorName { get; set; }

        [XmlArray("Units")]
        [XmlArrayItem("Unit")]
        public List<LeadInventoryUnit>? units { get; set; }
    }

    [Serializable]
    public class LeadInventoryTower
    {
        [XmlElement("TowerId")]
        public int towerId { get; set; }

        [XmlElement("TowerName")]
        public string? towerName { get; set; }

        [XmlArray("Floors")]
        [XmlArrayItem("Floor")]
        public List<LeadInventoryFloor>? floors { get; set; }
    }

    [Serializable]
    [XmlRoot("Towers")] 
    public class LeadInventoryWrap
    {
        [XmlElement("Tower")]
        public List<LeadInventoryTower>? towers { get; set; }
    }
}
