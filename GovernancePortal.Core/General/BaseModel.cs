﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Core.General
{
    public interface ICompanyModel
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public bool IsDeleted { get; set; }
        public ModelStatus ModelStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
    public class BaseModel : ICompanyModel
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public ModelStatus ModelStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }

    public enum ModelStatus
    {
        Default = 0,
        Draft = 1,
        Deleted = 10
    }
}
