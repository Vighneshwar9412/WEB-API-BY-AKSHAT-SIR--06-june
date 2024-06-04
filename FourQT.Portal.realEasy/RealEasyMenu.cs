using System.Runtime.Serialization;

namespace FourQT.Portal.RealEasy
{
    [DataContract]
    [Serializable]
    public class RealEasyMenu
    {
        [DataMember]
        public string? MenuID { get; set; }

        [DataMember]
        public string? Url { get; set; }

        [DataMember]
        public string? Name { get; set; }

        [DataMember]
        public string? ParentID { get; set; }

        [DataMember]
        public string? sPosition { get; set; }
    }
}
