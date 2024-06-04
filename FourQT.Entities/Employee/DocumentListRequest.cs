using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Employee
{
    public class DocumentListRequest : DocumentUploadCore2
    {
        public int registrationId { get; set; }
        public int projectId { get; set; }
    }

    public class DocumentListResponse : DocumentUploadCore
    {
        public int docId { get; set; }
        public string? docName { get; set; }
        public string? type { get; set; }
        public string? description { get; set; }
        public string? barcode { get; set; }
        public string? physicalStorageNo { get; set; }
        public string? scanRefNo { get; set; }
        public string? image { get; set; }
        public string? mandatory { get; set; }
        public Boolean? uploaded { get; set; }
        public Boolean? canDelete { get; set; }
    }

    public interface DocumentUploadCore
    {
        public int docId { get; set; }
        public string? barcode { get; set; }
        public string? physicalStorageNo { get; set; }
        public string? scanRefNo { get; set; }
        public string? image { get; set; }

    }

    public interface DocumentUploadCore2
    {
        public int registrationId { get; set; }
    }
}
