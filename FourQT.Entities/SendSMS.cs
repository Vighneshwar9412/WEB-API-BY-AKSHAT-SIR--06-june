using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class SendSMS
    {
        public int subjectId { get; set; }

        public string? body{ get; set; }

        public string? mobileNo{ get; set; }

        public string? otherMobile { get; set; }

        public int enquiryId { get; set; }

    }
}
