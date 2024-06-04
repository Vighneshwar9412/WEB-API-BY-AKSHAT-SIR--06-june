using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Portal
{
    [DataContract]
    [Serializable]
  public  class Notification
    {
        //public List<NotificationList> NotificationData { get; set; }
        //[DataMember]
        //public List<NotificationList> NotificationWiseList { get; set; }

        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string message { get; set; }
    }
}
