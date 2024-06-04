using System.Runtime.Serialization;

namespace FourQT.Entities
{
    [DataContract]
    [Serializable]
    public class ReceiptDetailsDataList
    {
        [DataMember]
        public string? title { get; set; }
        [DataMember]
        public string? receiptNumber { get; set; }

        [DataMember]
        public string? tagNameColor { get; set; }
        [DataMember]
        public string? Style { get; set; }
        [DataMember]
        public List<TagModalList>? ContentList { get; set; }
        [DataMember]
        public string? ReceiptLink { get; set; }

    }

}
