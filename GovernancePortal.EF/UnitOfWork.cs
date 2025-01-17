﻿using GovernancePortal.Data;
using GovernancePortal.Data.Repository;
using GovernancePortal.EF.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using GovernancePortal.Core.Resolutions;

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
            Votings = new VotingRepo(_context);
            Polls = new PollRepo(_context);
            Bridges = new BridgeRepo(_context);
        }

        public ITaskRepo Tasks { get; set; }
        public IMeetingRepo Meetings { get; }
        public IVotingRepo Votings { get; }
        public IPollRepo Polls { get; }
        public IBridgeRepo Bridges { get; }

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
