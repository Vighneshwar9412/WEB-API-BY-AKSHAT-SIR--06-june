using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Employee
{
    public class HoldUnitRequest : HoldUnitCore
    {
        public int unitId { get; set; }
        public int channelPartnerId { get; set; }
        public string? remarks { get; set; }
    }

    public class HoldUnitRequestU : HoldUnitCore
    {
        public int unitId { get; set; }
        public int channelPartnerId { get; set; }
        public string? remarks { get; set; }
    }

    public interface HoldUnitCore
    {
        public int unitId { get; set; }
        public int channelPartnerId { get; set; }
        public string? remarks { get; set; }
    }
}
