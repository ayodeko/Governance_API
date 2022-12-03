using System.Collections.Generic;
using System.Threading.Tasks;
using GovernancePortal.Core.Resolutions;
using GovernancePortal.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace GovernancePortal.EF.Repository;

public class VotingRepo : GenericRepo<Voting>, IVotingRepo
{
    public VotingRepo(DbContext context) : base(context)
    {
    }

    public Task<Voting> GetVoting(string resolutionId, string companyId)
    {
        throw new System.NotImplementedException();
    }

    public Task<Voting> GetVoting_Attendees(string resolutionId, string companyId)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<Voting> GetVotingList(string companyId, int pageNumber, int pageSize, out int totalRecords)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<Voting> SearchVotingByTitleList(string title, string companyId, int pageNumber, int pageSize, out int totalRecords)
    {
        throw new System.NotImplementedException();
    }
}