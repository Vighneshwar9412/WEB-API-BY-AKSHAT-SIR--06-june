using System.Runtime.Serialization;

namespace FourQT.Entities.Portal
{
    [DataContract]
    [Serializable]
    public class ChangePassword
    {
        [DataMember]
        public string? Token { get; set; }

        [DataMember]
        public string? UserName { get; set; }

        [DataMember]
        public string? OldPassword { get; set; }

        [DataMember]
        public string? NewPassword { get; set; }
    }

    public class ChangePasswordNew
    {
        public string? UserName { get; set; }

        public string? OldPassword { get; set; }

        public string? NewPassword { get; set; }
    }
}
