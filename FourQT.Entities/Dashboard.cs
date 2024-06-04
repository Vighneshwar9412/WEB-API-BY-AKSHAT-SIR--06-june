using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class Dashboard
    {
        public  int totalleadRecords { get; set; }
        public int totalsvRecords { get; set; }
        public List<LeadIcon> leadIconList { get; set; }=new List<LeadIcon>();
        public List<Dashboard_TodayLeadsNew> todayTodoLeadsList { get; set; } = new List<Dashboard_TodayLeadsNew>();
        public List<SiteVisitNew> todaySiteVisitScheduledList { get; set; } =new List<SiteVisitNew>();
    }
}
