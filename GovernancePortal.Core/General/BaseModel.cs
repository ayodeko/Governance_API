using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Core.General
{
    public interface ICompanyModel
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public bool IsDeleted { get; set; }
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
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
