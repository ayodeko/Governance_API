using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Service.ClientModels.General
{
    public class UploadConfig
    {
        public string AccessKey { get; set; }
        public string AccessSecret { get; set; }
        public string BucketName { get; set; }
    }


    public class UploadReturnObject : AttachmentIdentityDTO
    {
        public bool Success { get; set; }
    }
    public class GetPresignedUrlObject
    {
        public string Status { get; set; }
        public string PresignedUrl { get; set; }

    }
    public class DeleteReturnObject
    {
        public bool Success { get; set; }
        public string FileName { get; set; }
    }
}
