using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{

    public class ProjectItems
    { 
        public int itemId { get; set; }
            
        public string itemName { get; set;}

        public string itemPath { get; set;}

        public string extension { get; set;}    
    }

    public class SendEmail
    {
        public int subjectId { get; set; }
        public string? to { get; set; }

        public string? cc { get; set; }

        public string? bcc { get; set; }

        public string? body { get; set; }

        public string? subject { get; set; }

        public List<ProjectItems> documentsList { get; set; } = new List<ProjectItems>();

        public int enquiryId { get; set; }
    }
}
