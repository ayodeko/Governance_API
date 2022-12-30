using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovernancePortal.Core.General
{
    public class UserModel
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string ImageId { get; set; }
        public bool IsEnterpriseUser { get; set; }
        public bool IsPortfolioUser { get; set; }
        public bool IsStandardUser { get; set; }
    }
}
 