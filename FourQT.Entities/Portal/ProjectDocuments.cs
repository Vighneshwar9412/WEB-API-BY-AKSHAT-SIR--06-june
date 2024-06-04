using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FourQT.Entities.Portal;

namespace FourQT.Entities
{
    [DataContract]
    [Serializable]
    public class ProjectDocuments : CustomerLogin
    {
        [DataMember]
        public int ProjectId { get; set; }
        [DataMember]
        public int DocumentId { get; set; }
        [DataMember]
        public string DocType { get; set; }
        [DataMember]
        public int RegistrationId { get; set; }
    }

    [DataContract]
    [Serializable]
    public class ViewProjectDocuments
    {
        [DataMember]
        public List<CommonDocumentList> BrochureList { get; set; }

        [DataMember]
        public List<CommonDocumentList> LegalList { get; set; }

        [DataMember]
        public List<CommonDocumentList> PermissionList { get; set; }

        [DataMember]
        public List<Tabs> TabsList { get; set; }

        //[DataMember]
        //public string BrochureLabel { get; set; }

        //[DataMember]
        //public string LegalLabel { get; set; }

        //[DataMember]
        //public string PermissionLabel { get; set; }
    }

    [DataContract]
    [Serializable]
    public class CommonDocumentList
    {
        [DataMember]
        public int ProjectId { get; set; }
        [DataMember]
        public int DocumentId { get; set; }
        [DataMember]
        public string DocumentName { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string FileLink { get; set; }
        [DataMember]
        public string FileType { get; set; }
        [DataMember]
        public string FullFilePath { get; set; }
        [DataMember]
        public string PortalType { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Tabs
    {
        [DataMember]
        public string TabName { get; set; }

        [DataMember]
        public string TabLabel { get; set; }
    }

    public class CommonDocumentList_New 
    {
        public int projectId { get; set; }
        public int documentId { get; set; }
        public string? documentName { get; set; }
        public string? docType { get; set; }
        public string? fileName { get; set; }
        public string? fileLink { get; set; }
        public string? fileType { get; set; }
        public string? fullFilePath { get; set; }
        public string? portalType { get; set; }
    }
}
