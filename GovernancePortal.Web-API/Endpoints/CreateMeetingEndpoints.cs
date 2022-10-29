using System.Net;
using AutoMapper;
using GovernancePortal.Core.General;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Data;
using GovernancePortal.Data.Repository;
using GovernancePortal.EF;
using GovernancePortal.EF.Repository;
using GovernancePortal.Service.ClientModels.Exceptions;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GovernancePortal.Web_API.Endpoints;

public static class CreateMeetingEndpoints
{
    public static WebApplication MapSectionedMeetingEndpoints(this WebApplication app)
    {
        
        app.MapPost("api/Meeting/Create",
            ([FromServices] IMeetingServices meetingService, MeetingPOST createMeetingPost) =>
                meetingService.CreateMeeting(createMeetingPost));
        
        app.MapPost("api/Meeting/{meetingId}/Attendees/Update", ([FromServices] IMeetingServices meetingService,
                string meetingId, UpdateMeetingAttendeesPOST updateMeetingAttendeesPost) =>
            meetingService.UpdateAttendees(meetingId, updateMeetingAttendeesPost));
        
        app.MapPost("api/Meeting/{meetingId}/AgendaItems/Update", ([FromServices] IMeetingServices meetingService,
               string meetingId, UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPOST) =>
            meetingService.UpdateAgendaItems(meetingId, updateMeetingAgendaItemPOST));
        
        app.MapPost("api/Meeting/{meetingId}/MeetingPack/Update", ([FromServices] IMeetingServices meetingService,
                string meetingId, UpdateMeetingPackPOST updateMeetingPackPOST) =>
            meetingService.UpdateMeetingPack(meetingId, updateMeetingPackPOST));
        
        app.MapPost("api/Meeting/{meetingId}/Minutes/Update", ([FromServices] IMeetingServices meetingService,
                string meetingId, UpdateMeetingMinutesPOST updateMeetingMinutesPOST) =>
            meetingService.UpdateMinutes(meetingId, updateMeetingMinutesPOST));
        
        
        app.MapGet("api/Meeting/{meetingId}/Update", ([FromServices] IMeetingServices meetingService,
                string meetingId) =>
            meetingService.GetMeetingUpdateData(meetingId));
        
        app.MapGet("api/Meeting/{meetingId}/Minutes/Update", ([FromServices] IMeetingServices meetingService,
                string meetingId) =>
            meetingService.GetMeetingMinutesUpdateData(meetingId));
        
        app.MapGet("api/Meeting/{meetingId}/Attendees/Update", ([FromServices] IMeetingServices meetingService,
                string meetingId) =>
            meetingService.GetMeetingAttendeesUpdateData(meetingId));
        
        app.MapGet("api/Meeting/{meetingId}/AgendaItems/Update", ([FromServices] IMeetingServices meetingService,
                string meetingId) =>
            meetingService.GetMeetingAgendaItemsUpdateData(meetingId));
        
        app.MapGet("api/Meeting/{meetingId}/MeetingPack/Update", ([FromServices] IMeetingServices meetingService,
                string meetingId) =>
            meetingService.GetMeetingPackUpdateData(meetingId));
        
        return app;
    }

    public static WebApplicationBuilder RegisterCreateMeetingEndpointServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IMeetingServices, MeetingServices>();
        builder.Services.AddScoped<IMeetingMap, MeetingMap>();
        builder.Services.AddScoped<IUnits, Units>();
        return builder;
    }

    record CreateMeetingOnlyPOST();
}


interface IMeetingServices
{
    Task<Response> CreateMeeting(MeetingPOST createMeetingPOST);
    Task<Response> UpdateAttendees(string meetingId, UpdateMeetingAttendeesPOST updateMeetingAttendeesPost);
    Task<Response> UpdateAgendaItems(string meetingId, UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPOST);
    Task<Response> UpdateMeetingPack(string meetingId, UpdateMeetingPackPOST updateMeetingPackPOST);
    Task<Response> UpdateMinutes(string meetingId, UpdateMeetingMinutesPOST updateMinutesPOST);
    
    
    Task<Response> GetMeetingUpdateData(string meetingId);
    Task<Response> GetMeetingMinutesUpdateData(string meetingId);
    Task<Response> GetMeetingAttendeesUpdateData(string meetingId);
    Task<Response> GetMeetingAgendaItemsUpdateData(string meetingId);
    Task<Response> GetMeetingPackUpdateData(string meetingId);
}

interface IUnits
{
    IMeetingRepos Meeting { get; }
    int SaveToDB();
}

public class Units : IUnits
{
    private readonly PortalContext _context;

    public IMeetingRepos Meeting { get; }

    public Units(PortalContext context)
    {
        _context = context;
        Meeting = new MeetingRepos(_context);
    }


    public int SaveToDB()
    {
        return _context.SaveChanges();
    }
}

public interface IMeetingRepos : IGenericRepo<Meeting>
{
    Task<Meeting> GetMeeting_Attendees(string meetingId, string companyId);
}

class MeetingRepos : GenericRepo<Meeting>, IMeetingRepos
{
    public MeetingRepos(DbContext context) : base(context)
    {
    }

    public async Task<Meeting> GetMeeting_Attendees(string meetingId, string companyId)
    {
        return (await _context.Set<Meeting>()
            .Include(x => x.Attendees)
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId)))!;
    }
}
class MeetingsAutoMapper : Profile
{
    public MeetingsAutoMapper()
    {
        CreateMap<MeetingPOST, Meeting>();
        CreateMap<CreateMeetingAgendaItemDto, MeetingAgendaItem>();
        //CreateMap<MeetingModel, MeetingListGET>().ForMember(x => x.AttendanceId, option => option.MapFrom(y => y.Attendance.Id));
    }
}
interface IMeetingMap
{
    Meeting InMap(MeetingPOST createMeetingPost, Meeting meeting);
    Meeting InMap(UpdateMeetingAttendeesPOST updateMeetingAttendeesPost, Meeting meeting);
}

class MeetingMap : IMeetingMap
{
    private IMapper _autoMapper;
    public MeetingMap()
    {
        var profiles = new List<Profile>() { new MeetingsAutoMapper() };
        var mapperConfiguration = new MapperConfiguration(config => config.AddProfiles(profiles));
        _autoMapper = mapperConfiguration.CreateMapper();
    }
    public Meeting InMap(MeetingPOST source,  Meeting destination) => _autoMapper.Map(source, destination);
    
    #region Attending User Maps

    public Meeting InMap(UpdateMeetingAttendeesPOST updateMeetingAttendeesPost, Meeting meeting)
    {
        meeting.Attendees = InMap(updateMeetingAttendeesPost.Attendees, meeting);
        return meeting;
    }

    private List<AttendingUser> InMap(List<AttendingUserPOST> updateMeetingAttendeesPost, Meeting meeting)
    {
        var attendingUsers = new List<AttendingUser>();
        foreach (var attendingUserPost in updateMeetingAttendeesPost)
        {
            var attendingUser = InMap(attendingUserPost, meeting);
            attendingUsers.Add(attendingUser);
        }

        return attendingUsers;
    }

    private AttendingUser InMap(AttendingUserPOST updateMeetingAttendeesPost, Meeting meeting) => new AttendingUser()
        {
            UserId = updateMeetingAttendeesPost.UserId,
            Name = updateMeetingAttendeesPost.Name,
            AttendeePosition = updateMeetingAttendeesPost.AttendeePosition,
            IsGuest = updateMeetingAttendeesPost.IsGuest,
            IsPresent = updateMeetingAttendeesPost.IsPresent,
            MeetingId = meeting.Id,
            CompanyId = meeting.CompanyId,
        };
    #endregion
}

class MeetingServices : IMeetingServices
{
    Person GetLoggedUser()
    {
        return new Person();
    }

    private ILogger _logger;
    private IMeetingMap _meetingMaps;
    private IUnits _unit;

    public MeetingServices(IMeetingMap meetingMaps, ILogger logger, IUnits unitOfWork)
    {
        _meetingMaps = meetingMaps;
        _logger = logger;
        _unit = unitOfWork;
    }
    public async Task<Response> CreateMeeting(MeetingPOST createMeetingPOST)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation("Inside Create New Meeting");
        var meeting = _meetingMaps.InMap(createMeetingPOST, new Meeting());
        await _unit.Meeting.Add(meeting, loggedInUser);
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
        _logger.LogInformation("Inside Create New Meeting");
        var existingMeeting = await _unit.Meeting.GetMeeting_Attendees(meetingId, loggedInUser.CompanyId);
        var meeting = _meetingMaps.InMap(updateMeetingAttendeesPost, existingMeeting);
        
        if (meeting == null || meeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        
        await _unit.Meeting.Add(meeting, loggedInUser);
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

    public Task<Response> UpdateAgendaItems(string meetingId, UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPOST)
    {
        throw new NotImplementedException();
    }

    public Task<Response> UpdateMeetingPack(string meetingId, UpdateMeetingPackPOST updateMeetingPackPOST)
    {
        throw new NotImplementedException();
    }

    public Task<Response> UpdateMinutes(string meetingId, UpdateMeetingMinutesPOST updateMinutesPOST)
    {
        throw new NotImplementedException();
    }

    public Task<Response> GetMeetingUpdateData(string meetingId)
    {
        throw new NotImplementedException();
    }

    public Task<Response> GetMeetingMinutesUpdateData(string meetingId)
    {
        throw new NotImplementedException();
    }

    public Task<Response> GetMeetingAttendeesUpdateData(string meetingId)
    {
        throw new NotImplementedException();
    }

    public Task<Response> GetMeetingAgendaItemsUpdateData(string meetingId)
    {
        throw new NotImplementedException();
    }

    public Task<Response> GetMeetingPackUpdateData(string meetingId)
    {
        throw new NotImplementedException();
    }
}

class UpdateMeetingPackPOST
{
}
class UpdateMeetingAttendeesPOST
{
    public string MeetingId { get; set; }
    public List<AttendingUserPOST> Attendees { get; set; }
}
class UpdateMeetingAgendaItemPOST
{
}
class UpdateMeetingMinutesPOST
{
}










public class MeetingPOST
{
    public string Title { get; set; }
    public string Description { get; set; }
        
    public MeetingFrequency Frequency { get; set; }
    public MeetingMedium Medium { get; set; }
    public MeetingType Type { get; set; }
    public int Duration { get; set; }
    public DateTime DateTime { get; set; }
}


class AttendingUserPOST
{
    public new string Id { get; set; }
    public string UserId { get; set; }
    public string MeetingId { get; set; }
    public string MeetingAttendanceId { get; set; }
    public bool IsPresent { get; set; }
    public bool IsGuest { get; set; }
    public string Name { get; set; }
    public AttendeePosition AttendeePosition { get; set; }
}