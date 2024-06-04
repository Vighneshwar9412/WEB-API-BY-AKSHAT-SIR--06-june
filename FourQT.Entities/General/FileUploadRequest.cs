using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.General
{
    public class FileUploadRequest
    {
        public List<FileUpload>? files { get; set; } = new List<FileUpload>();
    }

    public class FileUploadResponse
    {
        public List<UploadedFile>? files { get; set; } = new List<UploadedFile>();
    }

    public class FileUploadAPIRequest
    {
        public List<FileUploadAPI>? files { get; set; } = new List<FileUploadAPI>();
    }

    public class FileUpload
    {
        public int id { get; set; }
        public string? fileName { get; set; }
        public string? fileFormat { get; set; }
        public string? fileBase64String { get; set; }
        public string? fileGroup { get; set; }
        public string? action { get; set; }
    }

    public class UploadedFile
    {
        public int id { get; set; }
        public Boolean fileUploaded { get; set; }
        public string? message { get; set; }
        public string? fileNameOnServer { get; set; }
        public string? filePathOnServer { get; set; }
    }

    public class FileUploadAPI : FileUpload
    {
        public string? filePath { get; set; }
    }

    public class ServerResponse
    {
        public Boolean isSuccess { get; set; }
        public string? message { get; set; }
        public FileUploadResponse? uploadedFiles { get; set; }
    }

}
