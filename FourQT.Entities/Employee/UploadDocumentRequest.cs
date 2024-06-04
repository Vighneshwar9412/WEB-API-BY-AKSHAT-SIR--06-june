using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Employee
{
    public class UploadDocumentRequest : DocumentUploadCore,DocumentUploadCore2
    {
        public int registrationId { get; set; }
        public int docId { get; set; }
        public string? barcode { get; set; }
        public string? physicalStorageNo { get; set; }
        public string? scanRefNo { get; set; }
        public string? image { get; set; }
        public string? docFormat { get; set; }
        public string? action { get; set; }
    }
}
