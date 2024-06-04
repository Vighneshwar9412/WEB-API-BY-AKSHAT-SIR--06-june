using System.Runtime.Serialization;

namespace FourQT.Entities
{
    [DataContract]
    [Serializable]
    public class TagModalListPayment
    {
        [DataMember]
        public string? tagName { get; set; }
        [DataMember]
        public string? tagValue { get; set; }
        [DataMember]
        public string? tagNameColor { get; set; }
        [DataMember]
        public string? Style { get; set; }

    }
}
