using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Employee
{
    public class SellInventoryRequest
    {
        public int unitId { get; set; }
        public string? customerName { get; set; }
        public string? customerPhoto { get; set; }
        public string? customerPhotoFormat { get; set; }
        public string? customerMobile { get; set; }
        public decimal amount { get; set; }
        public string? chequeCopy { get; set; }
        public string? chequeCopyFormat { get; set; }
    }
}
