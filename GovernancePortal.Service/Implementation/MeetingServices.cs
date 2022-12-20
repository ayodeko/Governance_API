﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using GovernancePortal.Core.General;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Data;
using GovernancePortal.Service.ClientModels.Exceptions;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.ClientModels.Meetings.Minute;
using GovernancePortal.Service.Interface;
using GovernancePortal.Service.Mappings.IMaps;
using Humanizer;
using Microsoft.Extensions.Logging;

namespace GovernancePortal.Service.Implementation;

public class MeetingServices : IMeetingService
{
    Person GetLoggedUser()
    {
        return new Person()
        {
            Id = "18312549-7133-41cb-8fd2-e76e1d088bb6",
            Name = "User1",
            CompanyId = "Company1",
            UserType = UserType.StandaloneUser
        };
    }

    private ILogger _logger;
    private IMeetingMaps _meetingMapses;
    private IUnitOfWork _unit;
    private readonly IValidator<Meeting> _meetingValidator;

    public MeetingServices(IMeetingMaps meetingMapses, ILogger logger, IUnitOfWork unitOfWork, IValidator<Meeting> meetingValidator)
    {
        _meetingMapses = meetingMapses;
        _logger = logger;
        _unit = unitOfWork;
        _meetingValidator = meetingValidator;
    }
    public async Task<Response> CreateMeeting(CreateMeetingPOST createMeetingPOST)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation("Inside Create New Meeting");
        var meeting = _meetingMapses.InMap(createMeetingPOST, new Meeting());
        await _meetingValidator.ValidateAndThrowAsync(meeting);
        meeting.ModelStatus = ModelStatus.Draft;
        await _unit.Meetings.Add(meeting, loggedInUser);
        var outMeeting = _meetingMapses.OutMap(meeting);
        _unit.SaveToDB();
        
        var response = new Response
        {
            Data = outMeeting,
            Message = "Meeting created successfully",
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("Create new meeting successful: {response}", response);
        return response;
    }
    public async Task<Response> UpdateMeetingDetails(string meetingId, UpdateMeetingPOST updateMeetingPost)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside update meeting for Id {meetingId}");
        var existingMeeting = await _unit.Meetings.FindById(meetingId, loggedInUser.CompanyId);
        if (existingMeeting is null || existingMeeting.ModelStatus == ModelStatus.Deleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        
        
        existingMeeting = _meetingMapses.InMap(updateMeetingPost, existingMeeting);
        await _meetingValidator.ValidateAndThrowAsync(existingMeeting);
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
    public async Task<Response> AddAttendees(string meetingId, AddAttendeesPOST addAttendeesPost)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside update Attendees for meeting {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_Attendees(meetingId, loggedInUser.CompanyId);
        
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        if (addAttendeesPost.Attendees == null || addAttendeesPost.Attendees.Count < 1)
            throw new Exception("Attendees list cannot be null or empty, ensure to select at least one participant");
        var duplicateUserId = CheckNonOfficialDuplicateAttendees(addAttendeesPost.Attendees);
        if (!string.IsNullOrEmpty(duplicateUserId))
            throw new Exception($"User Id {duplicateUserId} appears multiple times in list and has a non official Attendee position");
        
        addAttendeesPost.Attendees = addAttendeesPost.Attendees.DistinctBy(x => x.UserId).ToList();
        var attendingUserPosts = addAttendeesPost.Attendees.Select(x => new AttendingUserPOST()
        {
            UserId = x.UserId, Name = x.Name, AttendeePosition = x.AttendeePosition
        }).ToList();
        var attendingUsers = _meetingMapses.InMap(attendingUserPosts, existingMeeting);
        existingMeeting.Attendees = attendingUsers;
        existingMeeting.ChairPersonUserId = addAttendeesPost.ChairPersonUserId;
        existingMeeting.SecretaryUserId = addAttendeesPost.SecretaryUserId;
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
    
    string CheckNonOfficialDuplicateAttendees(List<AddAttendeesListPOST> attendeePostList)
    {
        var duplicateIdList = attendeePostList.GroupBy(x => x.UserId).Where(y => y.Count() > 1);
        var ids = duplicateIdList.Select(x => x.Key);
        foreach (var id in ids)
        {
            var idList = attendeePostList.Where(x => x.UserId == id);
            if (idList.All(x => x.AttendeePosition != AttendeePosition.MeetingOfficial))
                return id;
        }
        return null;
    }

    public async Task<Response> UpdateAttendingUsers(string meetingId, UpdateAttendingUsersPOST updateAttendingUsersPost)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside update Attendees for meeting {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_Attendees(meetingId, loggedInUser.CompanyId);
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        if (updateAttendingUsersPost.Attendees == null || updateAttendingUsersPost.Attendees.Count < 1)
            throw new Exception("Attendees list cannot be null or empty, ensure to select at least one participant");
        var duplicateUserId = CheckNonOfficialDuplicateAttendees(updateAttendingUsersPost.Attendees);
        if (!string.IsNullOrEmpty(duplicateUserId))
            throw new Exception($"User Id {duplicateUserId} appears multiple times in list and has a non official Attendee position");
        
        updateAttendingUsersPost.Attendees = updateAttendingUsersPost.Attendees.DistinctBy(x => x.UserId).ToList();
        var meeting = _meetingMapses.InMap(updateAttendingUsersPost, existingMeeting);
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
    
    string CheckNonOfficialDuplicateAttendees(List<AttendingUserPOST> attendeePostList)
    {
        var duplicateIdList = attendeePostList.GroupBy(x => x.UserId).Where(y => y.Count() > 1);
        var ids = duplicateIdList.Select(x => x.Key);
        foreach (var id in ids)
        {
            var idList = attendeePostList.Where(x => x.UserId == id);
            if (idList.All(x => x.AttendeePosition != AttendeePosition.MeetingOfficial))
                return id;
        }
        return null;
    }

    public async Task<Response> UpdateAgendaItems(string meetingId, UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPOST)
    {   
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside update Agenda Items for {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_AgendaItems(meetingId, loggedInUser.CompanyId);
        var meetingAgendaItems = _unit.Meetings.GetAgendaItems_With_MeetingHolder(meetingId, loggedInUser.CompanyId).ToList();
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var meeting = _meetingMapses.InMap(updateMeetingAgendaItemPOST, meetingAgendaItems, existingMeeting);
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

    public async Task<Response> FullUpdateAgendaItems(string meetingId, FullUpdateMeetingAgendaItemPOST updateMeetingAgendaItemPOST)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside update Agenda Items for {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_AgendaItems(meetingId, loggedInUser.CompanyId);
        var meetingAgendaItems = _unit.Meetings.GetAgendaItems_With_MeetingHolder(meetingId, loggedInUser.CompanyId).ToList();
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var meeting = _meetingMapses.InMap(updateMeetingAgendaItemPOST, meetingAgendaItems, existingMeeting);
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
        var existingMeeting = await _unit.Meetings.GetMeeting_AgendaItems_MeetingPack(meetingId, loggedInUser.CompanyId);
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        
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

    public async Task<Response> UpdateNotice(string meetingId, UpdateMeetingNoticePOST updateNoticePOST)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside update Minutes for {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_AgendaItems_Attendees_Notice(meetingId, loggedInUser.CompanyId);
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        
        existingMeeting = _meetingMapses.InMap(updateNoticePOST, existingMeeting);
        
        _unit.SaveToDB();
        
        var response = new Response
        {
            Data = existingMeeting,
            Message = "Meeting updated successfully",
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("Update Notice successful: {response}", response);
        return response;
    }

    public async Task<Response> GetMeetingUpdateData(string meetingId)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside get meetings for meetingId: {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting(meetingId, loggedInUser.CompanyId);
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var outMeeting = _meetingMapses.OutMap(existingMeeting);
        var response = new Response
        {
            Data = outMeeting,
            Message = "Meeting updated successfully",
            StatusCode = HttpStatusCode.OK.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("Get meeting update data successful: {response}", response);
        return response;
    }
   
    public async Task<Response> GetMeetingAttendeesUpdateData(string meetingId)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside get Meeting_Attendees update data for meeting {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_Attendees(meetingId, loggedInUser.CompanyId);
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var outMeeting = _meetingMapses.OutMap(existingMeeting, new UpdateAttendingUsersPOST());
        var response = new Response
        {
            Data = outMeeting,
            Message = "Successful",
            StatusCode = HttpStatusCode.OK.ToString(),
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
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var outMeeting = _meetingMapses.OutMap(existingMeeting, new UpdateMeetingAgendaItemPOST());
        var response = new Response
        {
            Data = outMeeting,
            Message = "Successful",
            StatusCode = HttpStatusCode.OK.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("Get Meeting Agenda Items Update Data successful: {response}", response);
        return response;
    }
    
    public async Task<Response> GetMeetingAgendaItemsFullUpdateData(string meetingId)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside get Meeting_AgendaItems update data for meeting {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_AgendaItems_Relationships(meetingId, loggedInUser.CompanyId);
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var outMeeting = _meetingMapses.OutMap(existingMeeting, new FullUpdateMeetingAgendaItemPOST());
        var response = new Response
        {
            Data = outMeeting,
            Message = "Successful",
            StatusCode = HttpStatusCode.OK.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("Get Meeting Agenda Items Update Data successful: {response}", response);
        return response;
    }

    public async Task<Response> GetMeetingPackUpdateData(string meetingId)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside get Meeting Pack update data for meeting {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_AgendaItems_MeetingPack(meetingId, loggedInUser.CompanyId);
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        List<UpdateMeetingPackItemPOST> outMeeting = default;
        if (existingMeeting.Packs is null || !existingMeeting.Packs.Any())
        {
            var newMeetingPackItems = await GenerateNewMeetingPack(existingMeeting, meetingId, loggedInUser.CompanyId);
            outMeeting = newMeetingPackItems.ToList();
        }
        else
        {
            outMeeting = _meetingMapses.OutMap(existingMeeting, new List<UpdateMeetingPackItemPOST>());
        }

        var meetingPack = new UpdateMeetingPackPOST
        {
            MeetingId = existingMeeting.Id,
            Packs = outMeeting
        };

        var response = new Response
        {
            Data = meetingPack,
            Message = "Successful",
            StatusCode = HttpStatusCode.OK.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("Get Meeting Pack Update Data successful: {response}", response);
        return response;
    }
    
    public async Task<Response> GetMeetingPack(string meetingId)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside get Meeting Pack update data for meeting {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_MeetingPack(meetingId, loggedInUser.CompanyId);
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var outMeeting = _meetingMapses.OutMap(existingMeeting, new MeetingGET());

        var response = new Response
        {
            Data = outMeeting,
            Message = "Successful",
            StatusCode = HttpStatusCode.OK.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("Get Meeting Pack Update Data successful: {response}", response);
        return response;
    }

    async Task<IEnumerable<UpdateMeetingPackItemPOST>> GenerateNewMeetingPack(Meeting meeting, string meetingId, string companyId)
    {
        var existingAgendaItemsMeeting =  meeting;
        if (existingAgendaItemsMeeting?.Items is null || !existingAgendaItemsMeeting.Items.Any())
        {
            throw new NotFoundException("Agenda Items are not found");
        }
        return existingAgendaItemsMeeting.Items.Select(x => new UpdateMeetingPackItemPOST
        {
            MeetingAgendaItemId = x.Id,
            Title = x.Title,
            RestrictedUsers = new()
            {
                new MeetingPackUserPOST()
            },
            InterestTagUsers = new(),
            CoCreators = new()
        });
        
    }

    public async Task<Response> GetMeetingNoticeUpdateData(string meetingId)
    {
        var loggedInUser = GetLoggedUser();
        var existingMeeting = await _unit.Meetings.GetMeeting_AgendaItems_Attendees_Notice(meetingId, loggedInUser.CompanyId);
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        UpdateMeetingNoticePOST outMeetingNotice = default;
        if (existingMeeting.Notice is null || existingMeeting.Notice.IsDeleted)
        {
            outMeetingNotice = GenerateNewMeetingNoticeData(existingMeeting);
        }
        else
        {
            var relations = GenerateNewMeetingNoticeData(existingMeeting);
            outMeetingNotice = _meetingMapses.OutMap(existingMeeting.Notice, new UpdateMeetingNoticePOST());
            outMeetingNotice.Attendees = relations.Attendees;
            outMeetingNotice.AgendaItems = relations.AgendaItems;
            outMeetingNotice.MeetingDate = existingMeeting.DateTime;
        }
        var response = new Response
        {
            Data = outMeetingNotice,
            Message = "Successful",
            StatusCode = HttpStatusCode.OK.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("Get Meeting Notice Update Data successful: {response}", response);
        return response;
    }

    public UpdateMeetingNoticePOST GenerateNewMeetingNoticeData(Meeting existingMeeting) => new UpdateMeetingNoticePOST
        {
            Attendees = existingMeeting.Attendees.Select(x => new AttendingUserPOST
            {
                Name = x.Name,
                UserId = x.UserId,
                Id = x.Id,
                AttendeePosition = x.AttendeePosition,
                IsPresent = x.IsPresent
            } ).ToList(),
            AgendaItems = existingMeeting.Items.Select(y => new BaseAgendaItemGET()
            {
                AgendaItemId = y.Id,
                Title = y.Title
            }).ToList(),
            //Venue = existingMeeting.Venue,
            MeetingDate = existingMeeting.DateTime
        };

    public async Task<Pagination<MeetingListGET>> GetAllMeetingList(int? meetingType, PageQuery pageQuery)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation("Inside get all meetings, {pageQuery}", pageQuery);
        var type = meetingType != null ?  (Enum.IsDefined(typeof(MeetingType), meetingType) ? (MeetingType)meetingType : throw new Exception("Wrong meeting type passed as query parameter")) : MeetingType.Board;
        var allMeetings = (meetingType == null)
            ? _unit.Meetings.GetMeetingList(loggedInUser.CompanyId, pageQuery.PageNumber,
                pageQuery.PageSize, out var totalRecords) 
            :  _unit.Meetings.GetMeetingListByMeetingType(type, loggedInUser.CompanyId,
            pageQuery.PageNumber, pageQuery.PageSize, out totalRecords);
        var allMeetingsList = allMeetings.ToList();
        var meetingListGet = _meetingMapses.OutMap(allMeetingsList);
        return new Pagination<MeetingListGET>
        {
            Data = meetingListGet.AsEnumerable(),
            PageNumber = pageQuery.PageNumber,
            PageSize = pageQuery.PageSize,
            TotalRecords = totalRecords,
            IsSuccessful = true,
            Message = "Successful",
            StatusCode = HttpStatusCode.OK.ToString()
        };
    }

    public async Task<Pagination<MeetingListGET>> GetUserMeetingList(PageQuery pageQuery)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside get Meeting Pack update data for user {loggedInUser.Id}");
        var existingMeetings = _unit.Meetings.GetMeetingListByUserId(loggedInUser.Id, loggedInUser.CompanyId, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords);
        var existingMeetingList = existingMeetings.ToList();
        if (existingMeetings is null || !existingMeetingList.Any()) throw new NotFoundException($"No Meeting with user Id: {loggedInUser.Id} was found");
        var outMeetingList = existingMeetingList.Select(x => _meetingMapses.OutMap(x, new MeetingListGET()));
        var response = new Pagination<MeetingListGET>
        {
            Data = outMeetingList.AsEnumerable(),
            PageNumber = pageQuery.PageNumber,
            PageSize = pageQuery.PageSize,
            TotalRecords = totalRecords,
            IsSuccessful = true,
            Message = "Successful",
            StatusCode = HttpStatusCode.OK.ToString()
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

    //public async Task<Response> GetMeetingDetails()
    //minutes
    public Task<Response> GetMeetingMinutesUpdateData(string meetingId)
    {

        throw new NotImplementedException();
    }
    //public async Task<Response> GetMeetingMinutesData(string meetingId)
    //{

    //    var loggedInUser = GetLoggedUser();
    //    _logger.LogInformation($"Inside get minutes data for meeting {meetingId}");
    //    var existingMeeting = await _unit.Meetings.GetMeeting_Minutes(meetingId, loggedInUser.CompanyId);
    //    if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
    //    var outMeeting = _meetingMapses.OutMap(existingMeeting, new MeetingGET());

    //    var response = new Response
    //    {
    //        Data = outMeeting,
    //        Message = "Successful",
    //        StatusCode = HttpStatusCode.OK.ToString(),
    //        IsSuccessful = true
    //    };
    //    _logger.LogInformation("Get Meeting Pack Update Data successful: {response}", response);
    //    return response;
    //}
    public async Task<Response> UpdateMinutes(string meetingId, UpdateMeetingMinutesPOST updateMinutesPOST)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside update Minutes for {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_AgendaItems_MeetingPack(meetingId, loggedInUser.CompanyId);
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");

        var meeting = _meetingMapses.InMap(updateMinutesPOST, existingMeeting);
        existingMeeting.Minutes = meeting.Minutes;
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
    public async Task<Response> AddMinutes(string meetingId, AddMinutePOST data)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside add Minutes for {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting(meetingId, loggedInUser.CompanyId);
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");

        var meeting = _meetingMapses.InMap(data, existingMeeting);
        existingMeeting.Minutes = meeting.Minutes;
        _unit.SaveToDB();
        var response = new Response
        {
            Data = existingMeeting,
            Message = "minute added successfully",
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("Add Minutes successful: {response}", response);
        return response;
    }

    public async Task<Response> UploadMinutes(string meetingId, UploadMinutePOST data)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside upload Minutes for {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting(meetingId, loggedInUser.CompanyId);
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");

        var meeting = _meetingMapses.InMap(data, existingMeeting);
        existingMeeting.Minutes = meeting.Minutes;
        _unit.SaveToDB();
        var response = new Response
        {
            Data = existingMeeting,
            Message = "Meeting uploaded successfully",
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("Upload Minutes successful: {response}", response);
        return response;
    }

    public Task<Response> GetMeetingDetails(string meetingId)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> GetMeetingMinutes(string meetingId)
    {

        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside get Minutes for meeting with id: {meetingId}");
        var existingMeeting = await _unit.Meetings.GetMeeting_Minutes(meetingId, loggedInUser.CompanyId);
        if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        if(existingMeeting.Minutes is null || !existingMeeting.Minutes.Any()) throw new BadRequestException($"This meeting has no uploaded minutes");
        var minutes = _meetingMapses.OutMap(existingMeeting.Minutes, new MinuteGET());
        var response = new Response
        {
            Data = minutes,
            Message = "Successful",
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("Get Minutes successful: {response}", response);
        return response;
    }

    public Task<Pagination<MeetingListGET>> GetAllMeetingList(PageQuery pageQuery)
    {
        throw new NotImplementedException();
    }

    public Task<Pagination<MeetingListGET>> GetAllMeetingList(int meetingType, PageQuery pageQuery)
    {
        throw new NotImplementedException();
    }
}
