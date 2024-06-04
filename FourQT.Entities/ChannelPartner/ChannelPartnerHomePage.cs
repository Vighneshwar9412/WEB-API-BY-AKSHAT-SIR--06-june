using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.ChannelPartner
{
    public class ChannelPartnerHomePage
    {
        
    }

    public class CPHome
    {
        public int projectId { get; set; }
        public string? projectName { get; set; }
        public string? area { get; set; }
        public int unit { get; set; }
        public string? totalSaleValue { get; set; }
        public int approved { get; set; }
        public int pending { get; set; }
        public int rejected { get; set; }
    }

    public class CPMasters
    {
        public List<CPMasterListSelect>? project { get; set; } = new List<CPMasterListSelect>();
        public List<CPMasterList>? salutation { get; set; } = new List<CPMasterList>();
        public List<CPMasterList>? tower { get; set; } = new List<CPMasterList>();
        public List<CPMasterList>? baseFloor { get; set; } = new List<CPMasterList>();
        public List<CPMasterListEmp>? salesEmpList { get; set; } = new List<CPMasterListEmp>();
    }

    public class CPMasterList
    {
        public int id { get; set; }
        public string? name { get; set; }
    }

    public class CPMasterListEmp : CPMasterList
    {
        public string? empMobile { get; set; }
    }

    public class CPMasterListSelect : CPMasterList
    {
        public int selected { get; set; }
    }
}
