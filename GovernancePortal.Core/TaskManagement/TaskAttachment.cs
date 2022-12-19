using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovernancePortal.Core.TaskManagement
{
    public class TaskAttachment
    {
        public TaskAttachment()
        {
            Id = Guid.NewGuid().ToString();
        }

        //system generated 
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string CategoryId { get; set; }
        public string TaskItemId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string CreatedBy { get; set; }

        //class properties
        public string Title { get; set; }

        //files Identity
        public string FileId { get; set; }
        public string VersionNumber { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FileType { get; set; }
    }
}
