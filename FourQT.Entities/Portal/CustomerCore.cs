
using System.Runtime.Serialization;


namespace FourQT.Entities.Portal
{
    [DataContract]
    [Serializable]
    internal class CustomerCore
    {
    }

    public class CustomerCore1
    {
        [DataMember]
        public int CustomerId { get; set; }
    }

    public class CustomerCore2
    {
        [DataMember]
        public int RegistrationId { get; set; }
    }

    public class CustomerCore4
    {
        [DataMember]
        public int PortalId { get; set; }
    }

    public class CustomerCore5 : CustomerCore2
    {
        [DataMember]
        public string? Type { get; set; }
    }

    public class CustomerCore2_2
    {
        [DataMember]
        public int Registration_Id { get; set; }
    }

    public class CustomerDoc : CustomerCore1
    {
        [DataMember]
        public string? DocType { get; set; }
    }
}
