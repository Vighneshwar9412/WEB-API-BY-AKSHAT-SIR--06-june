using System.Runtime.Serialization;

namespace FourQT.Entities
{
    [DataContract]
    [Serializable]
    public class MultipleApplicant
    {
        [DataMember]
        public string? Name { get; set; }
        [DataMember]
        public string? MobileNo { get; set; }
        [DataMember]
        public string? Photo { get; set; }
        [DataMember]
        public string? Address { get; set; }
        [DataMember]
        public string? EmailId { get; set; }
        [DataMember]
        public string? PanNo { get; set; }
        [DataMember]
        public string? AdharNo { get; set; }
        [DataMember]
        public string? DOB { get; set; }

        [DataMember]
        public string? S_W_D_O { get; set; }
        [DataMember]
        public string? Nationality { get; set; }
        [DataMember]
        public string? AnniversaryDate { get; set; }

    }
}
