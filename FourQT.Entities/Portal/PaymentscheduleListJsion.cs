using System.Runtime.Serialization;

namespace FourQT.Entities
{
    [DataContract]
    [Serializable]
    public class PaymentscheduleListJsion
    {
        [DataMember]
        public string? InstallmentName { get; set; }
        [DataMember]
        public string? InstallmentColor { get; set; }
        [DataMember]
        public string? InstallmentStyle { get; set; }

        [DataMember]
        public List<TagModalListPayment>? ContentList { get; set; }
    }

}
