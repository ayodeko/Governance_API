using GovernancePortal.Data;
using GovernancePortal.Data.Repository;
using GovernancePortal.EF.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PortalContext _context;

        public UnitOfWork(PortalContext context)
        {
            _context = context;
            Tasks = new TaskRepo(_context);
            Meetings = new MeetingRepo(_context);
        }

        public ITaskRepo Tasks { get; set; }
        public IMeetingsRepo Meetings { get; }

        public int SaveToDB()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
