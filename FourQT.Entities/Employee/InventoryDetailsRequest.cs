using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Employee
{
    public class InventoryDetailsResponseSoldBooked 
    {
        public int registrationId { get; set; }
        public string? registrationNo { get; set; }
        public int unitId { get; set; }    
        public string? unitNo { get; set; }
        public string? unitType { get; set; }
        public string? tower { get; set; }
        public string? floor { get; set; }      
        public string? customerName { get; set; }
        public string? customerPhoto { get; set; }
        public string? customerMobile { get; set; }
        public decimal bookingAmount { get; set; }
        public string? chequeCopy { get; set; }
        public string? bookingDate { get; set; }
        public string? soldBy { get; set; }
        public string? type { get; set; }
        public string? colorCode { get; set; }
        public int projectId { get; set; }
        public string? superArea { get; set; }
        public string? superAreaLabel { get; set; }
        public Boolean superAreaDisplay { get; set; }
        public string? carpetArea { get; set; }
        public string? carpetAreaLabel { get; set; }
        public Boolean carpetAreaDisplay { get; set; }
        public string? builtUpArea { get; set; }
        public string? builtUpAreaLabel { get; set; }
        public Boolean builtUpAreaDisplay { get; set; }
    }
}
