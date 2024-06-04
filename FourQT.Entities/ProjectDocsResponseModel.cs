using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class ProjectDocs
    {
        public int projectId { get; set; }

        public int itemId { get; set; }

        public string? projectName { get; set;}

        public string? documentType { get; set;}

        public string? fileName { get; set;}

        public string? itemName { get; set;}

        public string? itemPath { get; set;}

        public string? extension { get; set; }

    }

    public class ProjectDocsResponseModel
    {
        public List<ProjectDocs> projectdocsSources { get; set; } = new List<ProjectDocs>();

    }
}
