using System.Runtime.Serialization;

namespace FourQT.Portal.RealEasy
{   
    [DataContract]
    [Serializable]
    public class RealEasyLogin
    {
        [DataMember]
        public int status { get; set; }
        [DataMember]
        public List<RealEasyLoginDetails>? RealEasyLoginDetailsList { get; set; }
        [DataMember]
        public List<RealEasyEmailDetails>? RealEasyEmailDetailsList { get; set; }
        [DataMember]
        public List<RealEasyLoginStatus>? RealEasyLoginStatus { get; set; }
    }
}
