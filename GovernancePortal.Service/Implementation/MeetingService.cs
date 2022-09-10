using System;
using System.Threading.Tasks;
using GovernancePortal.Core.General;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Data;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.ClientModels.TaskManagement;
using GovernancePortal.Service.Interface;
using GovernancePortal.Service.Mappings.IMaps;

namespace GovernancePortal.Service.Implementation
{
    public class MeetingService : IMeetingService
    {
        private IMeetingMaps _meetingMaps;
        private IUnitOfWork _unit;
        
        public MeetingService(IMeetingMaps meetingMaps)
        {
            _meetingMaps = meetingMaps;
        }
        public async Task<MeetingModel> CreateMeeting(Person user, CreateMeetingPOST meetingDto)
        {
            var meeting = _meetingMaps.InMap(meetingDto, new MeetingModel());
            await _unit.Meetings.Add(meeting, user);
            _unit.SaveToDB();
            return meeting;
        }

        public async Task<MeetingModel> UpdateMeeting(string meetingId, Person user, UpdateMeetingPOST meetingDto)
        {
            var existingMeeting = await _unit.Meetings.FindById(meetingId, user.CompanyId);
            if (existingMeeting == null || existingMeeting.IsDeleted)
                throw new Exception($"Meeting with Id: {meetingId} not found");
            existingMeeting = _meetingMaps.InMap(meetingDto, existingMeeting);
            _unit.SaveToDB();
            return existingMeeting;
        }
    }
}