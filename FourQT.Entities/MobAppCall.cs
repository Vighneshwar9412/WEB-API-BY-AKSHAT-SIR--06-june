using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class MobAppCall
    {
        public List<MobAppCallModel>? callList { get; set; } = new List<MobAppCallModel>();
    }

    public class MobAppCallModel
    {
        public string? customerMobile { get; set; }
        public string? type { get; set; }
        public string? timeStart { get; set; }
        public string? timeEnd { get; set; }
        public int callDuration { get; set; }
    }

    public class MobAppCallReportReq : MobAppCallModel
    {
        public int callDurationTo { get; set; }
    }

    public class MobAppCallReport 
    {
        public int loginId { get; set; }
        public string? loginName { get; set; }
        public string? customerMobile { get; set; }
        public string? type { get; set; }
        public string? timeStart { get; set; }
        public string? timeEnd { get; set; }
        public string? callDuration { get; set; }
    }

    public class MobAppCallReportWrap {
        public List<MobAppCallReport>? CallList { get; set; } = new List<MobAppCallReport>();
    }
}
