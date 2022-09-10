using GovernancePortal.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskRepo Tasks { get; }
        IMeetingsRepo Meetings { get; }
        int SaveToDB();

    }
}
