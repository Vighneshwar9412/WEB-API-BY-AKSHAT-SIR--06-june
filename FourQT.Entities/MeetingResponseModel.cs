using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class MeetingResponseModel
    {
        public List<Meeting> meetingsources { get; set; } = new List<Meeting>();
    }

    public class Meeting
    {
        public string Name { get; set; }
        public string FollowedBy { get; set; }
        public DateTime? MeetingDatetime { get; set; }
        public string Duration { get; set; }

        public string Project_Name { get; set; }

        public string Location { get; set; }
    }
}
