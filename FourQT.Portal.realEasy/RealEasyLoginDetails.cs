using System.Runtime.Serialization;
using FourQT.Entities.Portal;

namespace FourQT.Portal.RealEasy
{
    [DataContract]
    [Serializable]
    public class RealEasyLoginDetails
    {
        [DataMember]
        public List<TagModalList>? LoginDetailsContentList { get; set; }
    }
}
