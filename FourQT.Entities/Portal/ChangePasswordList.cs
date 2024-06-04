using System.Runtime.Serialization;

namespace FourQT.Entities.Portal
{
    [DataContract]
    [Serializable]
    public class ChangePasswordList
    {
        [DataMember]
        public string? UserName { get; set; }

        [DataMember]
        public string? Password { get; set; }

    }
}
