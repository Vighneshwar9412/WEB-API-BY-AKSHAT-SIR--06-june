using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Employee
{
    public class InventoryPage
    {
        public int totalRecords { get; set; }
        public List<InventoryUnitDetails>? unit { get; set; } = new List<InventoryUnitDetails>();
    }

    public class InventoryPageSoldBooked
    {
        public int totalRecords { get; set; }
        public List<InventoryDetailsResponseSoldBooked>? unit { get; set; } = new List<InventoryDetailsResponseSoldBooked>();
    }
}
