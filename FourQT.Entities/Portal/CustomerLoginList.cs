using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace FourQT.Entities.Portal
{
    [DataContract]
    [Serializable]
    public class CustomerLoginList
    {
        [DataMember]
        public string? Token { get; set; }
        [DataMember]
        public int RegistrationId { get; set; }
        [DataMember]
        public string? UnitAddress { get; set; }
        [DataMember]
        public string? Type { get; set; } // -- A all, d demanded, N non demanded, O overdue, due - due       

        [DataMember]
        public string? Reg_No { get; set; }

    }

    public class CustomerLoginListNew : CustomerLoginList { 
        public string? unitImage {  get; set; }
        public string? unitIcon {  get; set; }
    }
}
