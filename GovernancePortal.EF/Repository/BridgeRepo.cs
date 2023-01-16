using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovernancePortal.Core.Bridges;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Core.Resolutions;
using GovernancePortal.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace GovernancePortal.EF.Repository
{
    public class BridgeRepo : IBridgeRepo
    {
        private readonly PortalContext _context;

        public BridgeRepo(PortalContext context)
        {
            _context = context;
        }

        public async Task<Meeting_Resolution> RetrieveMeeting_Resolution(string meetingId, string resolutionId)
        {
            return await _context.Set<Meeting_Resolution>()
                .FirstOrDefaultAsync(x => x.MeetingId == meetingId && x.ResolutionId == resolutionId);
        }
        public async Task<Meeting_Resolution> RetrieveMeetingByResolutionId(string resolutionId, string companyId)
        {
            return await _context.Set<Meeting_Resolution>()
                .FirstOrDefaultAsync(x => x.ResolutionId == resolutionId && x.CompanyId == companyId);
        }
        public IEnumerable<string> GetResolutionIdsMeetingId(string meetingId, string companyId)
        {
            return _context.Set<Meeting_Resolution>()
                .Where(x => x.MeetingId == meetingId && x.CompanyId == companyId)
                .Select(x => x.ResolutionId);
        }
        public IEnumerable<Voting> GetVotingsByMeetingId(string meetingId, string companyId)
        {
            var result =  _context.Set<Meeting>()
                .Join(_context.Meeting_Resolutions, meeting => meeting.Id, bridge => bridge.MeetingId,
                    (meeting, bridge) => new { Meeting = meeting, Bridge = bridge })
                .GroupJoin(_context.Votings, voteBridge => voteBridge.Bridge.ResolutionId, voting => voting.Id,
                    (voteBridge, voting) => new { voteBridge.Meeting, Voting = voting, voteBridge.Bridge })
                .SelectMany(bridge => bridge.Voting.DefaultIfEmpty(), 
                    (bridge, voting) => new {bridge.Bridge, Voting = voting} )
                .Where(x => x.Bridge.MeetingId == meetingId && x.Bridge.CompanyId == companyId).
                Select( x=> x.Voting);
            return result;
        }
        public IEnumerable<Poll> GetPollsByMeetingId(string meetingId, string companyId)
        {
            var result =  _context.Set<Meeting>()
                .Join(_context.Meeting_Resolutions, meeting => meeting.Id, bridge => bridge.MeetingId,
                    (meeting, bridge) => new { Meeting = meeting, Bridge = bridge })
                .GroupJoin(_context.Polls, voteBridge => voteBridge.Bridge.ResolutionId, poll => poll.Id,
                    (voteBridge, poll) => new { voteBridge.Meeting, Poll = poll, voteBridge.Bridge })
                .SelectMany(bridge => bridge.Poll.DefaultIfEmpty(), 
                    (bridge, poll) => new {bridge.Bridge, Poll = poll} )
                .Where(x => x.Bridge.MeetingId == meetingId && x.Bridge.CompanyId == companyId).
                Select( x=> x.Poll);
            return result;
        }
    
        public async Task AddMeeting_Resolution(string meetingId, string resolutionId, string companyId)
        {
            var retrievedMeeting_Resolution = await _context.Set<Meeting_Resolution>()
                .FirstOrDefaultAsync(x => x.MeetingId == meetingId && x.ResolutionId == resolutionId);
            if (retrievedMeeting_Resolution == null)
            {
                await _context.Set<Meeting_Resolution>().AddAsync(new Meeting_Resolution()
                {
                    MeetingId = meetingId,
                    ResolutionId = resolutionId,
                    CompanyId = companyId
                });
            }
            else
            {
                throw new Exception(
                    $"Bridge connection already of meetingId: {meetingId} and resolutionId: {resolutionId} already exists");
            }
        }
    
        public async Task AddTask_Resolution(string taskId, string resolutionId, string companyId)
        {
            var retrievedMeeting_Resolution = await _context.Set<Task_Resolution>()
                .FirstOrDefaultAsync(x => x.TaskId == taskId && x.ResolutionId == resolutionId);
            if (retrievedMeeting_Resolution == null)
            {
                await _context.Set<Task_Resolution>().AddAsync(new Task_Resolution()
                {
                    TaskId = taskId,
                    ResolutionId = resolutionId,
                    CompanyId = companyId
                });
            }
            else
            {
                throw new Exception(
                    $"Bridge connection for taskId: {taskId} and resolutionId: {resolutionId} already exists");
            }
        }
        
        
        public async Task<Task_Resolution> RetrieveTaskByResolutionId(string resolutionId, string companyId)
        {
            return await _context.Set<Task_Resolution>()
                .FirstOrDefaultAsync(x => x.ResolutionId == resolutionId && x.CompanyId == companyId);
        }
    
    }
}