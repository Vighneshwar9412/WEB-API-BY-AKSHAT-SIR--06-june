using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FourQT.Entities.Employee
{
    public class InventoryUnitDetails
    {
        public int unitId { get; set; }
        public string? project { get; set; }
        public string? tower { get; set; }
        public string? floor { get; set; }
        public string? unitNo { get; set; }
        public string? unitGroup { get; set; }
        public string? unitType { get; set; }       
        public string? location { get; set; }
        public string? unitPlan { get; set; }
        public string? floorPlan { get; set; }
        public string? status { get; set; }
        public string? cpName { get; set; }
        public string? holdDate { get; set; }
        public string? holdByEmployee { get; set; }
        public string? remarks { get; set; }
        public string? colorCode { get; set; }
        public string? customerName { get; set; }
        public string? customerMobile { get; set; }
        public string? superArea { get; set; }
        public string? superAreaLabel { get; set; }
        public Boolean superAreaDisplay { get; set; }
        public string? carpetArea { get; set; }
        public string? carpetAreaLabel { get; set; }
        public Boolean carpetAreaDisplay { get; set; }
        public string? buildupArea { get; set; }
        public string? buildupAreaLabel { get; set; }
        public Boolean buildupAreaDisplay { get; set; }
        public string? uId { get; set; }

    }

    public class UnitList
    {
        public List<InventoryUnitDetails>? unit { get; set; } = new List<InventoryUnitDetails>();
    }
}
