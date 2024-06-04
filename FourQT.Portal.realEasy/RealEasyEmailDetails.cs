using System.Runtime.Serialization;
using FourQT.Entities.Portal;

namespace FourQT.Portal.RealEasy
{
    [DataContract]
    [Serializable]
   public class RealEasyEmailDetails
   {
        [DataMember]
        public List<TagModalList>? EmailDetailContentList { get; set; }
    }
}
