using System.Runtime.Serialization;
using FourQT.Entities.Portal;

namespace FourQT.Portal.RealEasy
{
    [DataContract]
    [Serializable]
    public class RealEasyLoginStatus
    {
        [DataMember]
        public List<TagModalList>? LoginStatusContentList { get; set; }
    }
}
