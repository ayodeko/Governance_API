using System.Collections.Generic;
using System.Threading.Tasks;
using GovernancePortal.Core.Resolutions;
using GovernancePortal.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GovernancePortal.EF.Repository;

public class PollRepo : GenericRepo<Poll>, IPollRepo
{
    public PollRepo(DbContext context) : base(context)
    {
    }

    public async Task<Poll> GetPoll_PollVotersAsync(string resolutionId, string companyId)
    {
        return await _context.Set<Poll>()
            .Include(x => x.PollItems)
            .Include(x => x.PollUsers)
            .Include(x => x.PastPollItems)
            .FirstOrDefaultAsync(x => x.Id == resolutionId && x.CompanyId == companyId);
    }

    public IEnumerable<Poll> GetPoll_PollVotersList(string companyId, int pageNumber, int pageSize, out int totalRecords)
    {
        var skip = (pageNumber - 1) * pageSize;
        var votingList = _context.Set<Poll>()
            .Include(x => x.PollItems)
            .Include(x => x.PollUsers)
            .Include(x => x.PastPollItems)
            .Where(x => x.CompanyId == companyId);
        totalRecords = votingList.Count();
        return votingList.Skip(skip)
            .Take(pageSize)!;
    }
}