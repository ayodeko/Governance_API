using System.Collections.Generic;
using System.Threading.Tasks;
using GovernancePortal.Core.Bridges;

namespace GovernancePortal.Data.Repository;

 public interface IBridgeRepo
    {
        Task<Meeting_Resolution> RetrieveMeeting_Resolution(string meetingId, string resolutionId);
        Task AddMeeting_Resolution(string meetingId, string resolutionId, string companyId);
        Task<Meeting_Resolution> RetrieveMeetingByResolutionId(string resolutionId, string companyId);
        IEnumerable<string> GetResolutionIdsMeetingId(string meetingId, string companyId);
    }
