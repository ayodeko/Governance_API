using System.Collections.Generic;
using System.Threading.Tasks;
using GovernancePortal.Core.Bridges;
using GovernancePortal.Core.Resolutions;

namespace GovernancePortal.Data.Repository;

 public interface IBridgeRepo
    {
        Task<Meeting_Resolution> RetrieveMeeting_Resolution(string meetingId, string resolutionId);
        Task AddMeeting_Resolution(string meetingId, string resolutionId, string companyId);
        Task AddTask_Resolution(string taskId, string resolutionId, string companyId);
        Task<Meeting_Resolution> RetrieveMeetingByResolutionId(string resolutionId, string companyId);
        Task<Task_Resolution> RetrieveTaskByResolutionId(string resolutionId, string companyId);
        IEnumerable<string> GetResolutionIdsMeetingId(string meetingId, string companyId);
        IEnumerable<Poll> GetPollsByMeetingId(string meetingId, string companyId);
        IEnumerable<Voting> GetVotingsByMeetingId(string meetingId, string companyId);
    }
