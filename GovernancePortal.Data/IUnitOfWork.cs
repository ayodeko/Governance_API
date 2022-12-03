using GovernancePortal.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskRepo Tasks { get; }
        IMeetingRepo Meetings { get; }
        IVotingRepo Votings { get; }
        int SaveToDB();

    }
}
