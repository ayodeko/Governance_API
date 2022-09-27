using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GovernancePortal.Core.General;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Data;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.ClientModels.TaskManagement;
using GovernancePortal.Service.Interface;
using GovernancePortal.Service.Mappings.IMaps;
using Microsoft.Extensions.Logging;

namespace GovernancePortal.Service.Implementation
{
    public class MeetingService : IMeetingService
    {
        private IMeetingMaps _meetingMaps;
        private IUnitOfWork _unit;
        private ILogger _logger;
        
        public MeetingService(IMeetingMaps meetingMaps, ILogger logger)
        {
            _meetingMaps = meetingMaps;
            _logger = logger;
        }
        
        private Person GetLoggedUser()
        {
            return new Person()
            {
                Id = Guid.NewGuid().ToString(),
                CompanyId = Guid.NewGuid().ToString(),
                Name = "Abebefe Idris",
            };
        }
        public async Task<Response> CreateMeeting(CreateMeetingPOST meetingDto)
        {
            var loggedInUser = GetLoggedUser();
            _logger.LogInformation("Inside Create New Meeting");
            var meeting = _meetingMaps.InMap(meetingDto, new MeetingModel());
            await _unit.Meetings.Add(meeting, loggedInUser);
            _unit.SaveToDB();
            
            var response = new Response
            {
                Data = meeting,
                Message = "Meeting created successfully",
                StatusCode = HttpStatusCode.Created.ToString(),
                IsSuccessful = true
            };
            _logger.LogInformation("Create new meeting successful: {response}", response);
            return response;
        }

        public async Task<Pagination<MeetingListGET>> GetAllMeetings(PageQuery pageQuery)
        {
            var loggedInUser = GetLoggedUser();
            _logger.LogInformation("Inside get all meetings, {pageQuery}", pageQuery);
            var allMeetings = await _unit.Meetings.FindByPage(loggedInUser.CompanyId, pageQuery.PageNumber, pageQuery.PageSize);
            var meetingListGet = _meetingMaps.OutMap(allMeetings.ToList(), new List<MeetingListGET>());
            var totalRecords = await _unit.Meetings.Count(loggedInUser.CompanyId);
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

        public async Task<Response> UpdateMeeting(string meetingId, UpdateMeetingPOST meetingDto)
        {
            var loggedInUser = GetLoggedUser();
            _logger.LogInformation("Inside Update Meeting, {ID}", meetingId);
            var existingMeeting = await _unit.Meetings.FindById(meetingId, loggedInUser.CompanyId);
            if (existingMeeting == null || existingMeeting.IsDeleted)
                throw new Exception($"Meeting with Id: {meetingId} not found");
            existingMeeting = _meetingMaps.InMap(meetingDto, existingMeeting);
            _unit.SaveToDB();
            
            var response = new Response
            {
                Data = existingMeeting,
                Message = "Meeting Updated successfully",
                StatusCode = HttpStatusCode.Created.ToString(),
                IsSuccessful = true
            };
            _logger.LogInformation("Updated meeting {ID} successful: {response}", meetingId, response);
            return response;
        }
        
        public async Task<Response> AddPastMeeting(AddPastMeetingPOST meetingDto)
        {
            var loggedInUser = GetLoggedUser();
            var meeting = _meetingMaps.InMap(meetingDto, new MeetingModel());
            await _unit.Meetings.Add(meeting, loggedInUser);
            _unit.SaveToDB();
            var response = new Response
            {
                Data = meeting,
                Message = "Past meeting added successfully",
                StatusCode = HttpStatusCode.Created.ToString(),
                IsSuccessful = true
            };
            _logger.LogInformation("Added past meeting successfully {response}", response);
            return response;
        }
        public async Task<Response> AddPastMinutes(AddPastMinutesPOST meetingDto)
        {
            Person loggedInUser = GetLoggedUser();
            _logger.LogInformation("Inside AddPastMinutes");
            var meeting = _meetingMaps.InMap(meetingDto, new MeetingModel());
            await _unit.Meetings.Add(meeting, loggedInUser);
            _unit.SaveToDB();
            
            var response = new Response
            {
                Data = meeting,
                Message = "Past minutes added successfully",
                StatusCode = HttpStatusCode.Created.ToString(),
                IsSuccessful = true
            };
            _logger.LogInformation("Added past minutes successfully {response}", response);
            return response;
        }

        public async Task<Response> AddPastAttendance(AddPastAttendancePOST meetingDto)
        {
            var loggedUser = GetLoggedUser();
            _logger.LogInformation("Inside AddPastMinutes");
            var meeting = _meetingMaps.InMap(meetingDto, new MeetingModel());
            await _unit.Meetings.Add(meeting, loggedUser);
            _unit.SaveToDB();
            
            var response = new Response
            {
                Data = meeting,
                Message = "Past attendance added successfully",
                StatusCode = HttpStatusCode.Created.ToString(),
                IsSuccessful = true
            };
            _logger.LogInformation("Added past attendance successfully {response}", response);
            return response;
        }
        
        
        
        
        
        public async Task<Response> GetMeetingById(string meetingId)
        {
            var loggedInUser = GetLoggedUser();
            var existingMeeting = await _unit.Meetings.FindById(meetingId, loggedInUser.CompanyId);
            if (existingMeeting == null || existingMeeting.IsDeleted)
                throw new Exception($"Meeting with Id: {meetingId} not found");
            var meetingDto = _meetingMaps.OutMap(existingMeeting, new MeetingGET());
            var response = new Response
            {
                Data = meetingDto,
                Message = $"Meeting with Id: {meetingId} retrieved successfully",
                StatusCode = HttpStatusCode.Created.ToString(),
                IsSuccessful = true
            };
            return response;
        }

        

        async Task SendGeneratedCode(string generatedCode)
        {
        }
        string GenerateAttendanceCode()
        {
            throw new NotImplementedException();
        }
    }
}