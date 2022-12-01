using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GovernancePortal.Core.General;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Data;
using GovernancePortal.Service.ClientModels.Exceptions;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.Interface;
using GovernancePortal.Service.Mappings.IMaps;
using Microsoft.Extensions.Logging;

namespace GovernancePortal.Service.Implementation;


public class MeetingServices : IMeetingService
{
    Person GetLoggedUser()
    {
        return new Person
        {
            Id = "CompanyId1",
            CompanyId = "Company1UserAdmin1"
        };
    }

    private ILogger _logger;
    private IMeetingMaps _meetingMapses;
    private IUnitOfWork _unit;

    public MeetingServices(IMeetingMaps meetingMapses, ILogger logger, IUnitOfWork unitOfWork)
    {
        _meetingMapses = meetingMapses;
        _logger = logger;
        _unit = unitOfWork;
    }
    public async Task<Response> CreateMeeting(MeetingPOST createMeetingPOST)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation("Inside Create New Meeting");
        var meeting = _meetingMapses.InMap(createMeetingPOST, new Meeting());
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
    public async Task<Response> UpdateAttendees(string meetingId, UpdateMeetingAttendeesPOST updateMeetingAttendeesPost)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside update Attendees for meeting {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_Attendees(meetingId, loggedInUser.CompanyId);
        if (existingMeeting == null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var meeting = _meetingMapses.InMap(updateMeetingAttendeesPost, existingMeeting);
        existingMeeting.Attendees = meeting.Attendees;
        _unit.SaveToDB();
        
        var response = new Response
        {
            Data = existingMeeting,
            Message = "Meeting updated successfully",
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("UpdateAttendees successful: {response}", response);
        return response;
    }

    public async Task<Response> UpdateAgendaItems(string meetingId, UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPOST)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside update Agenda Items for {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_AgendaItems(meetingId, loggedInUser.CompanyId);
        if (existingMeeting == null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var meeting = _meetingMapses.InMap(updateMeetingAgendaItemPOST, existingMeeting);
        existingMeeting.Items = meeting.Items;
        _unit.SaveToDB();
        
        var response = new Response
        {
            Data = existingMeeting,
            Message = "Meeting updated successfully",
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("UpdateAgendaItems successful: {response}", response);
        return response;
    }

    public async Task<Response> UpdateMeetingPack(string meetingId, UpdateMeetingPackPOST updateMeetingPackPOST)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside update Meeting Pack for {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_MeetingPack(meetingId, loggedInUser.CompanyId);
        if (existingMeeting == null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        
        var meeting = _meetingMapses.InMap(updateMeetingPackPOST, existingMeeting);
        existingMeeting.Packs = meeting.Packs;
        _unit.SaveToDB();
        
        var response = new Response
        {
            Data = existingMeeting,
            Message = "Meeting updated successfully",
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("UpdateMeetingPack successful: {response}", response);
        return response;
    }

    public async Task<Response> UpdateMinutes(string meetingId, UpdateMeetingMinutesPOST updateMinutesPOST)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside update Minutes for {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_MeetingPack(meetingId, loggedInUser.CompanyId);
        if (existingMeeting == null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        
        var meeting = _meetingMapses.InMap(updateMinutesPOST, existingMeeting);
        existingMeeting.Packs = meeting.Packs;
        
        await _unit.Meetings.Add(meeting, loggedInUser);
        _unit.SaveToDB();
        
        var response = new Response
        {
            Data = existingMeeting,
            Message = "Meeting updated successfully",
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("UpdateMinutes successful: {response}", response);
        return response;
    }

    public async Task<Response> GetMeetingUpdateData(string meetingId)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside get meetings for meetingId: {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_AllDependencies(meetingId, loggedInUser.CompanyId);
        if (existingMeeting == null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var outMeeting = _meetingMapses.OutMap(existingMeeting);
        var response = new Response
        {
            Data = outMeeting,
            Message = "Meeting updated successfully",
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("Get meeting update data successful: {response}", response);
        return response;
    }

    public Task<Response> GetMeetingMinutesUpdateData(string meetingId)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> GetMeetingAttendeesUpdateData(string meetingId)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside get Meeting_Attendees update data for meeting {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_Attendees(meetingId, loggedInUser.CompanyId);
        if (existingMeeting == null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var outMeeting = _meetingMapses.OutMap(existingMeeting, new UpdateMeetingAttendingUserGET());
        var response = new Response
        {
            Data = outMeeting,
            Message = "Successful",
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("Get Meeting Attendees Update Data successful: {response}", response);
        return response;
    }

    public async Task<Response> GetMeetingAgendaItemsUpdateData(string meetingId)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside get Meeting_AgendaItems update data for meeting {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_AgendaItems(meetingId, loggedInUser.CompanyId);
        if (existingMeeting == null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var outMeeting = _meetingMapses.OutMap(existingMeeting, new UpdateMeetingAgendaItemGET());
        var response = new Response
        {
            Data = outMeeting,
            Message = "Successful",
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("Get Meeting Agenda Items Update Data successful: {response}", response);
        return response;
    }

    public async Task<Response> GetMeetingPackUpdateData(string meetingId)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside get Meeting Pack update data for meeting {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_MeetingPack(meetingId, loggedInUser.CompanyId);
        if (existingMeeting == null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var outMeeting = _meetingMapses.OutMap(existingMeeting, new UpdateMeetingAttendingUserGET());
        var response = new Response
        {
            Data = outMeeting,
            Message = "Successful",
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("Get Meeting Pack Update Data successful: {response}", response);
        return response;
    }

    public async Task<Pagination<MeetingListGET>> GetAllMeetingList(PageQuery pageQuery)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation("Inside get all meetings, {pageQuery}", pageQuery);
        var allMeetings = await _unit.Meetings.FindByPage(loggedInUser.CompanyId, pageQuery.PageNumber, pageQuery.PageSize);
        var allMeetingsList = allMeetings.ToList();
        var meetingListGet = _meetingMapses.OutMap(allMeetingsList);
        var totalRecords = await _unit.Meetings.Count(loggedInUser.CompanyId);
        return new Pagination<MeetingListGET>
        {
            Data = meetingListGet.AsEnumerable(),
            PageNumber = pageQuery.PageNumber,
            PageSize = pageQuery.PageSize,
            TotalRecords = totalRecords,
            IsSuccessful = true,
            Message = "Successful",
            StatusCode = "00"
        };
    }

    public async Task<Pagination<MeetingListGET>> GetUserMeetingList(PageQuery pageQuery)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside get Meeting Pack update data for user {loggedInUser.Id}");
        var existingMeetings = _unit.Meetings.GetMeetingListByUserId(loggedInUser.Id, loggedInUser.CompanyId, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords);
        var existingMeetingList = existingMeetings.ToList();
        if (existingMeetings == null || !existingMeetingList.Any()) throw new NotFoundException($"No Meeting with user Id: {loggedInUser.Id} was found");
        var outMeetingList = existingMeetingList.Select(x => _meetingMapses.OutMap(x, new MeetingListGET()));
        var response = new Pagination<MeetingListGET>
        {
            Data = outMeetingList.AsEnumerable(),
            PageNumber = pageQuery.PageNumber,
            PageSize = pageQuery.PageSize,
            TotalRecords = totalRecords,
            IsSuccessful = true,
            Message = "Successful",
            StatusCode = "00"
        };
        _logger.LogInformation("Get Meeting Pack Update Data successful: {response}", response);
        return response;
    }
    
    public async Task<Response> SearchMeetings(string meetingSearchString)
    {
        var loggedInUser = GetLoggedUser();
        var retrievedMeetings =  _unit.Meetings.FindBySearchString(meetingSearchString, loggedInUser.CompanyId).ToList();
        if (!retrievedMeetings.Any())
        {
            var failedResponse = new Response
            {
                Data = null,
                Message = $"Meetings that contain: {meetingSearchString} not found",
                StatusCode = HttpStatusCode.OK.ToString(),
                IsSuccessful = true
            };
            return failedResponse;
        }

        var meetingListGet = _meetingMapses.OutMap(retrievedMeetings);
        var response = new Response
        {
            Data = meetingListGet,
            Message = $"Retrieved successfully",
            StatusCode = HttpStatusCode.OK.ToString(),
            IsSuccessful = true
        };
        return response;
    }
    public async Task<Response> SearchMeetingsByDate(DateTime meetingDateTime)
    {
        var loggedInUser = GetLoggedUser();
        var retrievedMeetings =  _unit.Meetings.FindByDate(meetingDateTime, loggedInUser.CompanyId).ToList();
        if (!retrievedMeetings.Any())
        {
            var failedResponse = new Response
            {
                Data = null,
                Message = $"Meetings with date: {meetingDateTime.ToString(CultureInfo.CurrentCulture)} not found",
                StatusCode = HttpStatusCode.OK.ToString(),
                IsSuccessful = true
            };
            return failedResponse;
        }

        var meetingListGet = _meetingMapses.OutMap(retrievedMeetings);
        var response = new Response
        {
            Data = meetingListGet,
            Message = $"Retrieved successfully",
            StatusCode = HttpStatusCode.OK.ToString(),
            IsSuccessful = true
        };
        return response;
    }
    
}
