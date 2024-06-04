using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace FourQT.Entities.Portal
{
    [DataContract]
    [Serializable]
    public class CustomerLogin
    {
        [DataMember]
        public string? Token { get; set; }

        [DataMember]
        public string? username { get; set; }

        [DataMember]
        public string? password { get; set; }

        [DataMember]
        public string? DeviceId { get; set; }

        [DataMember]
        public int FromDevice { get; set; }

        [DataMember]      
        public int CustomerId { get; set; }

        [DataMember]
        public string? Type { get; set; }

        [DataMember]
        public int Status { get; set; }

    }

    public interface CustomerLoginCore
    {
        [DataMember]
        public string? Token { get; set; }

        [DataMember]
        public string? username { get; set; }

        [DataMember]
        public string? password { get; set; }

        [DataMember]
        public string? DeviceId { get; set; }

        [DataMember]
        public int FromDevice { get; set; }

    }

    public interface CustomerLoginAdd
    {
        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public string? Type { get; set; }

        [DataMember]
        public int Status { get; set; }
    }

    public class CustomerLoginShort : CustomerLoginCore
    {
        [DataMember]
        public string? Token { get; set; }

        [DataMember]
        public string? username { get; set; }

        [DataMember]
        public string? password { get; set; }

        [DataMember]
        public string? DeviceId { get; set; }

        [DataMember]
        public int FromDevice { get; set; }
    }

    public class CustomerLoginLong : CustomerLoginCore, CustomerLoginAdd
    {
        [DataMember]
        public string? Token { get; set; }

        [DataMember]
        public string? username { get; set; }

        [DataMember]
        public string? password { get; set; }

        [DataMember]
        public string? DeviceId { get; set; }

        [DataMember]
        public int FromDevice { get; set; }

        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public string? Type { get; set; }

        [DataMember]
        public int Status { get; set; }
    }

}
