using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.ChannelPartner
{
    public class TowerFloorListModel
    {
        public List<CPMasterList>? tower { get; set; } = new List<CPMasterList>();
        public List<CPMasterList>? floor { get; set; } = new List<CPMasterList>();
    }
}
