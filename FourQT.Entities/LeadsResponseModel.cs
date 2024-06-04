using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class LeadIcon
    {
        public int loginid { get; set; }
        public string? status { get; set; }
        public string? count { get; set; }
        public string? iconpath { get; set; }
        public int sortOrder { get; set; }
    }

    public class LeadsResponseModel
    {
        public List<LeadIcon>? leadsSources { get; set; } = new List<LeadIcon>();

        
    }
}
