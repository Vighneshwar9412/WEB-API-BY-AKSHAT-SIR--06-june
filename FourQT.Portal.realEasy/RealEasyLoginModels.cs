using System.Runtime.Serialization;

namespace FourQT.Portal.RealEasy
{
    [DataContract]
    [Serializable]
   public class RealEasyLoginModels
    {
        [DataMember]
        public string? Token { get; set; }

        [DataMember]
        public string? username { get; set; }

        [DataMember]
        public string? password { get; set; }
    }
}
