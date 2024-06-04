using System.Runtime.Serialization;

namespace FourQT.Entities.Portal
{
    [DataContract]
    [Serializable]
    public class CustomerDemand
    {
        [DataMember]
        public string? Token { get; set; }

        [DataMember]
        public int Registration_Id { get; set; }

        [DataMember]
        public string? InstallmentDate { get; set; }

        [DataMember]
        public int LetterId { get; set; }

        [DataMember]
        public int PortalId { get; set; }

        [DataMember]
        public int Queryid { get; set; }

        [DataMember]
        public int RegistrationId { get; set; }
    }
}
