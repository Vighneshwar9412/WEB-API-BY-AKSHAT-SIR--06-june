using System.Runtime.Serialization;

namespace FourQT.Entities.Portal
{
    [DataContract]
    [Serializable]
    public class ClientInfo
    {
        [DataMember]
        public string? Token { get; set; }

        [DataMember]
        public string? ClientName { get; set; }

        [DataMember]
        public string? ClientConnectionString { get; set; }

        [DataMember]
        public string? ClientURL { get; set; }

        [DataMember]
        public string? ClientFolderPath { get; set; }

        [DataMember]
        public string? HeaderColor { get; set; }

        [DataMember]
        public string? FooterColor { get; set; }

        [DataMember]
        public string? Logo { get; set; }

        [DataMember]
        public string? FontType { get; set; }

        [DataMember]
        public string? ThemeColor { get; set; }
    }
}
