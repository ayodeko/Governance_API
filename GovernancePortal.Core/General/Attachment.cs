using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovernancePortal.Core.General
{
    public class Attachment : BaseModel, ICompanyModel
    {
        public Attachment()
        {
            Id = Guid.NewGuid().ToString();
        }

        //system generated 
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string CategoryId { get; set; }
        //public Category DocCategoryType { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string CreatedBy { get; set; }


        //class properties
        public string Highlight { get; set; }
        public string Source { get; set; }
        public bool HasExpiryDate { get; set; }
        public DateTime? ReferenceDate { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string ReferenceDescription { get; set; }
        public string StatusDescription { get; set; }
        public string OtherDetails { get; set; }


        //files Identity
        public string FileId { get; set; }
        public string VersionNumber { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FileType { get; set; }
        //public DateTime FileCreationTime { get; set; }
        //public DateTime? FileLastModificationTime { get; set; }

        //from class metadata
        public string DocumentStatus { get; set; }
        public string Reference { get; set; }






    }
}
