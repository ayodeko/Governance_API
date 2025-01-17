﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovernancePortal.Core.Resolutions;
using GovernancePortal.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace GovernancePortal.EF.Repository;

public class VotingRepo : GenericRepo<Voting>, IVotingRepo
{
    protected readonly DbContext _context;
    public VotingRepo(DbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Voting> GetVotingAsync(string resolutionId, string companyId)
    {
        return await _context.Set<Voting>()
            .FirstOrDefaultAsync(x => x.Id.Equals(resolutionId) && x.CompanyId.Equals(companyId));
    }

    public async Task<Voting> GetVoting_VotersAsync(string resolutionId, string companyId)
    {
        return await _context.Set<Voting>()
            .Include(x => x.Voters)
            .FirstOrDefaultAsync(x => x.Id == resolutionId && x.CompanyId == companyId);
    }

    public IEnumerable<Voting> GetVoting_VotersList(string companyId, string userId, string searchString, DateTime? dateTime, int pageNumber, int pageSize, out int totalRecords)
    {
        var skip = (pageNumber - 1) * pageSize;
        var votingList = _context.Set<Voting>()
            .Include(x => x.Voters)
            .Where(x => dateTime == null || x.DateTime == dateTime )
            .Where(x => string.IsNullOrEmpty(searchString) || x.Title.Contains(searchString))
            .Where(x => string.IsNullOrEmpty(userId) || x.Voters.Any(c => c.UserId == userId))
            .Where(x => x.CompanyId == companyId)
            .OrderByDescending(X =>X.DateCreated);
        totalRecords = votingList.Count();
        return votingList.Skip(skip)
            .Take(pageSize)!;
    }

    public IEnumerable<Voting> SearchVotingByTitleList(string title, string companyId, int pageNumber, int pageSize, out int totalRecords)
    {
        var skip = (pageNumber - 1) * pageSize;
        var votingList = _context.Set<Voting>()
            .Include(x => x.Voters)
            .Where(x => x.CompanyId == companyId && x.Title.Contains(title))
            .OrderByDescending(X =>X.DateCreated);
        totalRecords = votingList.Count();
        return votingList.Skip(skip)
            .Take(pageSize)!;
    }
}