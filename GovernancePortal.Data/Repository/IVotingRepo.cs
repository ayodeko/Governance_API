using System.Collections.Generic;
using System.Threading.Tasks;
using GovernancePortal.Core.Resolutions;

namespace GovernancePortal.Data.Repository;

public interface IVotingRepo : IGenericRepo<Voting>
{
    Task<Voting> GetVotingAsync(string resolutionId, string companyId);
    Task<Voting> GetVoting_VotersAsync(string resolutionId, string companyId);
    IEnumerable<Voting> GetVoting_VotersList(string companyId, int pageNumber, int pageSize,
        out int totalRecords);
    IEnumerable<Voting> SearchVotingByTitleList(string title, string companyId, int pageNumber, int pageSize,
        out int totalRecords);
    
}