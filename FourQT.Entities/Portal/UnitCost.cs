using System.Runtime.Serialization;

namespace FourQT.Entities
{
    [DataContract]
    [Serializable]
    public class UnitCost
    {
        [DataMember]
        public string? headingName { get; set; }
        [DataMember]
        public string? headingvalue { get; set; }
        [DataMember]
        public string? headingNameColor { get; set; }
        [DataMember]
        public string? headingNameStyle { get; set; }
        [DataMember]
        public List<TagModalList>? ContentList { get; set; }
    }
}
