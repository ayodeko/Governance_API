﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GovernancePortal.Core.Resolutions;

namespace GovernancePortal.Data.Repository;

public interface IPollRepo : IGenericRepo<Poll>
{
    Task<Poll> GetPoll_PollVotersAsync(string resolutionId, string companyId);
    IEnumerable<Voting> GetPoll_PollVotersList(string companyId, int pageNumber, int pageSize,
        out int totalRecords);
}