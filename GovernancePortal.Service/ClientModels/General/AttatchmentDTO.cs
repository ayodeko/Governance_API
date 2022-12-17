using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Service.ClientModels.General
{
    public class AttatchmentListGET
    {

    }
    public class AttachmentPostDTO : AttachmentDTO
    {

    }

    public class AttatchmentGetDTO: AttachmentDTO
    {

    }

    public class AttachmentDTO
    {
        public string Title { get; set; }
        public string Highlight { get; set; }
        public string Source { get; set; }
        public bool HasExpiryDate { get; set; }
        public DateTime? ReferenceDate { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string OtherDetails { get; set; }
        public AttachmentIdentityDTO Identity { get; set; }

        //from class metadata
        public string DocumentStatus { get; set; }
        public string Reference { get; set; }

        //for 'others' option on metadata
        public string ReferenceDescription { get; set; }
        public string StatusDescription { get; set; }
    }

    public class AttachmentIdentityDTO
    {
        public string FileId { get; set; }
        public int VersionNumber { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FileType { get; set; }
      
    }
}
