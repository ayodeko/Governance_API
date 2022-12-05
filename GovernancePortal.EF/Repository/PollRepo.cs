using System.Collections.Generic;
using System.Threading.Tasks;
using GovernancePortal.Core.Resolutions;
using GovernancePortal.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace GovernancePortal.EF.Repository;

public class PollRepo : GenericRepo<Poll>, IPollRepo
{
    public PollRepo(DbContext context) : base(context)
    {
    }

    public Task<Poll> GetPoll_PollVotersAsync(string resolutionId, string companyId)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<Voting> GetPoll_PollVotersList(string companyId, int pageNumber, int pageSize, out int totalRecords)
    {
        throw new System.NotImplementedException();
    }
}