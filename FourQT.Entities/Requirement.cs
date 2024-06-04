using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class Requirement
    {
        public int enquiryId { get; set; }
        public int channelId { get; set; }       
        public int cityId { get; set;}
        public int enqFrom { get; set; }
        public int budgetMin { get; set; }
        public int budgetMax { get; set; }
        public int saleableAreaMin { get; set; }
        public int saleableAreaMax { get; set; }
        public string? projectId { get; set; }
        public string? unitTypeId { get; set; }
    }
}
