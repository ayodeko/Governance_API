using GovernancePortal.Core.TaskManagement;
using GovernancePortal.Data.Repository;
using GovernancePortal.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovernancePortal.EF.Repository
{
    public class TaskRepo : GenericRepo<TaskModel>, ITaskRepo
    {
        public PortalContext _db { get { return _context as PortalContext; } }
        public TaskRepo(PortalContext db) : base(db)
        {

        }
    }
}
