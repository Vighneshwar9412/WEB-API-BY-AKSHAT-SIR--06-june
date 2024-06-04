using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class SVLocation : Lead
    {
      //  public int LoginId { get; set; }
      //  public int ChannelId { get; set; }
      //  public int EnquiryId { get; set; }
      public string Geolocation { get; set; }

    }

    public class SVLocationShort
    {
        public string? geolocation { get; set; }

        public int enquiryId { get; set; }

        public int channelId { get; set; }
    }
}
