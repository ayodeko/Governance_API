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

        #region Create, Update Meeting
        
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
        #endregion
        
        #region Get Update Data for meeting
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
        
        #endregion
        app.MapGet("api/Meeting/UserMeetings", ([FromServices] IMeetingServices meetingService, PageQuery pageQuery) =>
            meetingService.GetUserMeetingList(pageQuery));
        
        
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
    
    
    Task<Response> GetUserMeetingList(PageQuery pageQuery);
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
    Task<Meeting> GetMeeting(string meetingId, string companyId);
    Task<Meeting> GetMeeting_AllDependencies(string meetingId, string companyId);
    Task<Meeting> GetMeeting_Attendees(string meetingId, string companyId);
    Task<Meeting> GetMeeting_AgendaItems(string meetingId, string companyId);
    Task<Meeting> GetMeeting_MeetingPack(string meetingId, string companyId);
    IEnumerable<Meeting> GetMeetingListByUserId(string userId, string companyId, PageQuery pageQuery);
}

class MeetingRepos : GenericRepo<Meeting>, IMeetingRepos
{
    public MeetingRepos(DbContext context) : base(context)
    {
    }

    public async Task<Meeting> GetMeeting(string meetingId, string companyId)
    {
        return (await _context.Set<Meeting>()
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId)))!;
    }
    public async Task<Meeting> GetMeeting_AllDependencies(string meetingId, string companyId)
    {
        return (await _context.Set<Meeting>()
            .Include(x => x.Attendees)
            .Include(x => x.Items)
            .Include(x => x.Packs)
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId)))!;
    }
    public async Task<Meeting> GetMeeting_Attendees(string meetingId, string companyId)
    {
        return (await _context.Set<Meeting>()
            .Include(x => x.Attendees)
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId)))!;
    }
    public async Task<Meeting> GetMeeting_AgendaItems(string meetingId, string companyId)
    {
        return (await _context.Set<Meeting>()
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId)))!;
    }
    public async Task<Meeting> GetMeeting_MeetingPack(string meetingId, string companyId)
    {
        return (await _context.Set<Meeting>()
            .Include(x => x.Packs)
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId)))!;
    }

    public IEnumerable<Meeting> GetMeetingListByUserId(string userId, string companyId, PageQuery pageQuery)
    {
        var skip = (pageQuery.PageNumber - 1) * pageQuery.PageSize;
        return  ( _context.Set<Meeting>()
            .Include(x => x.Attendees)
            .Where(x => x.CompanyId.Equals(companyId) && x.Attendees.Exists(c => c.UserId == userId))
            .Skip(skip)
            .Take(pageQuery.PageSize))!;
    }
}
class MeetingsAutoMapper : Profile
{
    public MeetingsAutoMapper()
    {
        CreateMap<MeetingPOST, Meeting>();
        CreateMap<Meeting, UpdateMeetingGET>();
        CreateMap<Meeting, MeetingListGET>();
        CreateMap<Meeting, UpdateMeetingAttendingUserGET>();
        CreateMap<Meeting, UpdateMeetingAgendaItemGET>();
        CreateMap<MeetingAgendaItem, AgendaItemPOST>();
        CreateMap<CreateMeetingAgendaItemDto, MeetingAgendaItem>();
        //CreateMap<MeetingModel, MeetingListGET>().ForMember(x => x.AttendanceId, option => option.MapFrom(y => y.Attendance.Id));
    }
}
interface IMeetingMap
{
    Meeting InMap(MeetingPOST createMeetingPost, Meeting meeting);
    Meeting InMap(UpdateMeetingAttendeesPOST updateMeetingAttendeesPost, Meeting meeting);
    Meeting InMap(UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPost, Meeting meeting);
    Meeting InMap(UpdateMeetingPackPOST updateMeetingPackPost, Meeting existingMeeting);
    Meeting InMap(UpdateMeetingMinutesPOST updateMinutesPost, Meeting existingMeeting);
    MeetingListGET OutMap(Meeting existingMeeting, MeetingListGET meetingList);

    UpdateMeetingAttendingUserGET OutMap(Meeting existingMeeting,
        UpdateMeetingAttendingUserGET updateMeetingAttendingUserGet);

    UpdateMeetingAgendaItemGET OutMap(Meeting existingMeeting, UpdateMeetingAgendaItemGET updateMeetingAgendaItemGet);
    UpdateMeetingGET OutMap(Meeting existingMeeting);
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

    public MeetingListGET OutMap(Meeting existingMeeting, MeetingListGET updateMeetingAttendingUserGet) => _autoMapper.Map(existingMeeting, new MeetingListGET());

    public UpdateMeetingGET OutMap(Meeting existingMeeting) => _autoMapper.Map(existingMeeting, new UpdateMeetingGET());
    
    #region Attending User Maps

    public UpdateMeetingAttendingUserGET OutMap(Meeting existingMeeting, UpdateMeetingAttendingUserGET updateMeetingAttendingUserGet) => _autoMapper.Map(existingMeeting, updateMeetingAttendingUserGet);

    public UpdateMeetingAgendaItemGET OutMap(Meeting existingMeeting,
        UpdateMeetingAgendaItemGET updateMeetingAgendaItemGet) =>
        _autoMapper.Map(existingMeeting, updateMeetingAgendaItemGet);

    public Meeting InMap(UpdateMeetingAttendeesPOST updateMeetingAttendeesPost, Meeting meeting)
    {
        meeting.Attendees = InMap(updateMeetingAttendeesPost.Attendees, meeting);
        return meeting;
    }

    public List<AttendingUser> InMap(List<AttendingUserPOST> updateMeetingAttendeesPost, Meeting meeting)
    {
        var attendingUsers = new List<AttendingUser>();
        foreach (var attendingUserPost in updateMeetingAttendeesPost)
        {
            var attendingUser = InMap(attendingUserPost, meeting);
            attendingUsers.Add(attendingUser);
        }

        return attendingUsers;
    }

    public AttendingUser InMap(AttendingUserPOST updateMeetingAttendeesPost, Meeting meeting) => new AttendingUser()
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
    
    #region Agenda Items
    
    public Meeting InMap(UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPost, Meeting meeting)
    {
        meeting.Items = InMap(updateMeetingAgendaItemPost.Items, meeting);
        return meeting;
    }

    public List<MeetingAgendaItem> InMap(List<AgendaItemPOST> updateMeetingAgendaItemPost, Meeting meeting)
    {
        if (updateMeetingAgendaItemPost == null || !updateMeetingAgendaItemPost.Any()) return null;
        var agendaItems = new List<MeetingAgendaItem>();
        foreach (var agendaItemPost in updateMeetingAgendaItemPost)
        {
            var agendaItem = InMap(agendaItemPost, meeting);
            agendaItems.Add(agendaItem);
        }

        return agendaItems;
    }

    public MeetingAgendaItem InMap(AgendaItemPOST updateMeetingAgendaItemPost, Meeting meeting)
    {
        var agendaItem =  new MeetingAgendaItem
        {
            MeetingId = meeting.Id,
            CompanyId = meeting.CompanyId,
            Title = updateMeetingAgendaItemPost.Title,
            Number = updateMeetingAgendaItemPost.Number,
            SubItems = InMap(updateMeetingAgendaItemPost.SubItems, meeting)
        };
        return agendaItem;
    }

    #endregion
    
    #region Meeting Pack Maps

    public Meeting InMap(UpdateMeetingPackPOST updateMeetingPackPost, Meeting existingMeeting)
    {
        existingMeeting.Packs = InMap(updateMeetingPackPost.Packs, existingMeeting);
        return existingMeeting;
    }

    private List<MeetingPackItem> InMap(List<UpdateMeetingPackItemPOST> meetingPacksPOST, Meeting existingMeeting)
    {
        var meetingPackItems = new List<MeetingPackItem>();
        foreach (var meetingPackPOST in meetingPacksPOST)
        {
            var meetingPackItem = InMap(meetingPackPOST, existingMeeting);
            meetingPackItems.Add(meetingPackItem);
        }

        return meetingPackItems;
    }

    private MeetingPackItem InMap(UpdateMeetingPackItemPOST updateMeetingPackPost, Meeting existingMeeting)
    {
        var meetingPackItem = new MeetingPackItem()
        {
            MeetingId = existingMeeting.Id,
            MeetingAgendaItemId = updateMeetingPackPost.AgendaItemId,
            PresenterUserId = updateMeetingPackPost.PresenterUserId,
            Description = updateMeetingPackPost.Description,
            Duration = updateMeetingPackPost.Duration,
            CoCreators = updateMeetingPackPost.CoCreators.Select(x => new MeetingPackItemUser
            {
                AttendingUserId = x
            }).ToList(),
            RestrictedUsers = updateMeetingPackPost.RestrictedUsers.Select(x => new MeetingPackItemUser
            {
                AttendingUserId = x
            }).ToList(),
            InterestTagUsers = updateMeetingPackPost.InterestTagUsers.Select(x => new MeetingPackItemUser
            {
                AttendingUserId = x
            }).ToList(),
        };
        return meetingPackItem;
    }

    #endregion

    #region Minutes Maps

    public Meeting InMap(UpdateMeetingMinutesPOST updateMinutesPost, Meeting existingMeeting)
    {
        return existingMeeting;
    }

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
        _logger.LogInformation($"Inside update Attendees for meeting {meetingId}");
        var existingMeeting = await _unit.Meeting.GetMeeting_Attendees(meetingId, loggedInUser.CompanyId);
        if (existingMeeting == null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var meeting = _meetingMaps.InMap(updateMeetingAttendeesPost, existingMeeting);
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
        var existingMeeting = await _unit.Meeting.GetMeeting_AgendaItems(meetingId, loggedInUser.CompanyId);
        if (existingMeeting == null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var meeting = _meetingMaps.InMap(updateMeetingAgendaItemPOST, existingMeeting);
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
        var existingMeeting = await _unit.Meeting.GetMeeting_MeetingPack(meetingId, loggedInUser.CompanyId);
        if (existingMeeting == null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        
        var meeting = _meetingMaps.InMap(updateMeetingPackPOST, existingMeeting);
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
        var existingMeeting = await _unit.Meeting.GetMeeting_MeetingPack(meetingId, loggedInUser.CompanyId);
        if (existingMeeting == null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        
        var meeting = _meetingMaps.InMap(updateMinutesPOST, existingMeeting);
        existingMeeting.Packs = meeting.Packs;
        
        await _unit.Meeting.Add(meeting, loggedInUser);
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
        var existingMeeting = await _unit.Meeting.GetMeeting_AllDependencies(meetingId, loggedInUser.CompanyId);
        if (existingMeeting == null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var outMeeting = _meetingMaps.OutMap(existingMeeting);
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
        var existingMeeting = await _unit.Meeting.GetMeeting_Attendees(meetingId, loggedInUser.CompanyId);
        if (existingMeeting == null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var outMeeting = _meetingMaps.OutMap(existingMeeting, new UpdateMeetingAttendingUserGET());
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
        var existingMeeting = await _unit.Meeting.GetMeeting_AgendaItems(meetingId, loggedInUser.CompanyId);
        if (existingMeeting == null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var outMeeting = _meetingMaps.OutMap(existingMeeting, new UpdateMeetingAgendaItemGET());
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
        var existingMeeting = await _unit.Meeting.GetMeeting_MeetingPack(meetingId, loggedInUser.CompanyId);
        if (existingMeeting == null || existingMeeting.IsDeleted) throw new NotFoundException($"Meeting with ID: {meetingId} not found");
        var outMeeting = _meetingMaps.OutMap(existingMeeting, new UpdateMeetingAttendingUserGET());
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
    
    public async Task<Response> GetUserMeetingList(PageQuery pageQuery)
    {
        var loggedInUser = GetLoggedUser();
        _logger.LogInformation($"Inside get Meeting Pack update data for user {loggedInUser.Id}");
        var existingMeetings = _unit.Meeting.GetMeetingListByUserId(loggedInUser.Id, loggedInUser.CompanyId, pageQuery);
        var existingMeetingList = existingMeetings.ToList();
        if (existingMeetings == null || !existingMeetingList.Any()) throw new NotFoundException($"No Meeting with user Id: {loggedInUser.Id} was found");
        var outMeetingList = existingMeetingList.Select(x => _meetingMaps.OutMap(x, new MeetingListGET()));
        var response = new Response
        {
            Data = outMeetingList,
            Message = "Successful",
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = true
        };
        _logger.LogInformation("Get Meeting Pack Update Data successful: {response}", response);
        return response;
    }
    
}

public class UpdateMeetingPackPOST
{
    public string MeetingId { get; set; }
    public List<UpdateMeetingPackItemPOST> Packs { get; set; }
}

public class UpdateMeetingPackItemPOST
{
    public string AgendaItemId { get; set; }
    public string MeetingId { get; set; }
    public string Description { get; set; }
    public string PresenterUserId { get; set; }
    public List<string> CoCreators { get; set; }
    public List<string> RestrictedUsers { get; set; }
    public List<string> InterestTagUsers { get; set; }
    public DateTime Duration { get; set; }
}

class UpdateMeetingAttendeesPOST
{
    public string MeetingId { get; set; }
    public List<AttendingUserPOST> Attendees { get; set; }
}
class UpdateMeetingAgendaItemPOST
{
    public string MeetingId { get; set; }
    public List<AgendaItemPOST> Items { get; set; }
}
public class UpdateMeetingAgendaItemGET
{
    public string MeetingId { get; set; }
    public List<AgendaItemPOST> Items { get; set; }
}
class UpdateMeetingMinutesPOST
{
}










public class MeetingBaseDto
{
    public string Title { get; set; }
    public string Description { get; set; }
        
    public MeetingFrequency Frequency { get; set; }
    public MeetingMedium Medium { get; set; }
    public MeetingType Type { get; set; }
    public int Duration { get; set; }
    public DateTime DateTime { get; set; }
}

public class MeetingPOST : MeetingBaseDto
{
    
}

public class MeetingListGET : MeetingBaseDto
{
    public string Id { get; set; }
    public List<AttendingUserPOST> Attendees { get; set; }
}

public class UpdateMeetingGET
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public MeetingFrequency Frequency { get; set; }
    public MeetingMedium Medium { get; set; }
    public MeetingType Type { get; set; }
    public int Duration { get; set; }
    public DateTime DateTime { get; set; }
}

public class UpdateMeetingAttendingUserGET
{
    public string MeetingId { get; set; }
    private List<AttendingUserPOST> Attendees { get; set; }
}
public class AttendingUserPOST
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
public class AgendaItemPOST
{
    public new string Id { get; set; }
    public string MeetingId { get; set; }
    public int Number { get; set; }
    public string Title { get; set; }
    public List<AgendaItemPOST> SubItems { get; set; }
}
public class SubAgendaItemPOST
{
    public string MeetingId { get; set; }
    public List<SubAgendaItemPOST> SubItems { get; set; }
}