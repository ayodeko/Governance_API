using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovernancePortal.Core.General;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Data;
using GovernancePortal.Service.ClientModels.General;
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

        public async Task<Pagination<MeetingListGET>> GetAllMeetings(string companyId, PageQuery pageQuery)
        {
            var allMeetings = await _unit.Meetings.FindByPage(companyId, pageQuery.PageNumber, pageQuery.PageSize);
            var meetingListGet = _meetingMaps.OutMap(allMeetings.ToList(), new List<MeetingListGET>());
            var totalRecords = await _unit.Meetings.Count(companyId);
            return new Pagination<MeetingListGET>
            {
                Data = meetingListGet,
                PageNumber = pageQuery.PageNumber,
                PageSize = pageQuery.PageSize,
                TotalRecords = totalRecords,
                IsSuccessful = true,
                Message = "Successful",
                StatusCode = "00"
            };
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
        
        public async Task<MeetingModel> AddPastMeeting(Person user, AddPastMeetingPOST meetingDto)
        {
            var meeting = _meetingMaps.InMap(meetingDto, new MeetingModel());
            await _unit.Meetings.Add(meeting, user);
            _unit.SaveToDB();
            return meeting;
        }
        public async Task<MeetingModel> AddPastMinutes(Person user, AddPastMinutesPOST meetingDto)
        {
            var meeting = _meetingMaps.InMap(meetingDto, new MeetingModel());
            await _unit.Meetings.Add(meeting, user);
            _unit.SaveToDB();
            return meeting;
        }
    }
}