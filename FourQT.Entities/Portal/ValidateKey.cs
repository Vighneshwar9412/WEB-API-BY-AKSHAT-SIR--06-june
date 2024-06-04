using System.Runtime.Serialization;

namespace FourQT.Entities.Portal
{
    [DataContract]
    [Serializable]
    public class ValidateKey
    {
        [DataMember]
        public string? eKey { get; set; }
    }
}
