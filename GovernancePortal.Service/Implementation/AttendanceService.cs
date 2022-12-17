using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using GovernancePortal.Core.General;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Data;
using GovernancePortal.Data.Repository;
using GovernancePortal.Service.ClientModels.Exceptions;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.Interface;
using Microsoft.Extensions.Logging;

namespace GovernancePortal.Service.Implementation;

public class AttendanceService : IAttendanceServices
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unit;
    private readonly IBusinessLogic _logic;

    public AttendanceService(ILogger logger, IBusinessLogic logic, IUnitOfWork unit)
    {
        _logger = logger;
        _unit = unit;
        _logic = logic;
    }
    public async Task<Response> GenerateAttendanceCode(string meetingId, CancellationToken token)
    {
        var user = GetLoggedUser();
        var code = GenerateAttendanceCode();
        var meeting = await _unit.Meetings.GetMeeting(meetingId, user.CompanyId);
        if (meeting == null || meeting.ModelStatus == ModelStatus.Deleted) throw new NotFoundException($"Meeting with Id: {meetingId} not found");
        meeting.AttendanceGeneratedCode = code;
        _unit.SaveToDB();
        var response = new Response
        {
            Data = code,
            Message = string.IsNullOrEmpty(code) ? "Could not generate code" : "Meeting created successfully" ,
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = !string.IsNullOrEmpty(code)
        };
        _logger.LogInformation("Create new meeting successful: {response}", response);
        
        return response;
    }
    
    string GenerateAttendanceCode()
    {
        return Guid.NewGuid().ToString().Split('-')[0].ToUpper();
    }
    
    public async Task<Response> RetrieveGeneratedAttendanceCode(string meetingId, CancellationToken token)
    {
        var user = GetLoggedUser();
        var meeting = await _unit.Meetings.GetMeeting(meetingId, user.CompanyId);
        if (meeting == null || meeting.ModelStatus == ModelStatus.Deleted) throw new NotFoundException($"Meeting with Id: {meetingId} not found");
        var code = meeting.AttendanceGeneratedCode;
        if (string.IsNullOrEmpty(code))
            throw new NotFoundException($"No generated attendance code yet for meeting with Id: {meetingId}");
        var response = new Response
        {
            Data = code,
            Message = "Code retrieved successfully" ,
            StatusCode = HttpStatusCode.OK.ToString(),
            IsSuccessful = !string.IsNullOrEmpty(code)
        };
        _logger.LogInformation("Create new meeting successful: {response}", response);
        
        return response;
    }
    
    public async Task<Response> SendAttendanceCodeToAll(string meetingId, CancellationToken token)
    {
        var loggedInUser = GetLoggedUser();
        var meeting = await _unit.Meetings.GetMeeting_Attendees(meetingId, loggedInUser.CompanyId);
        if (meeting == null || meeting.ModelStatus == ModelStatus.Deleted) throw new NotFoundException($"Meeting with Id: {meetingId} not found");
        var code = meeting?.AttendanceGeneratedCode;
        if (string.IsNullOrEmpty(code))
            throw new NotFoundException($"Retrieved attendance code for meeting {meetingId} is null or empty");
        var userIds = meeting.Attendees.Where(x => x.IsPresent == false).Select(x => x.UserId).ToList();
        var status = await _logic.SendMailToBulkUsersAsync(code, userIds, token);
        var response = new Response
        {
            Data = status,
            Message = status ? "Successfully sent code" : "Failed to send code" ,
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = status
        };
        return response;
    }
    
    

    public async Task<Response> SendAttendanceCodeToUser(string meetingId, string userId, CancellationToken token)
    {
        var loggedInUser = GetLoggedUser();
        var meeting = await _unit.Meetings.GetMeeting_Attendees(meetingId, loggedInUser.CompanyId);
        if (meeting == null || meeting.ModelStatus == ModelStatus.Deleted) throw new NotFoundException($"Meeting with Id: {meetingId} not found");
        var code = meeting?.AttendanceGeneratedCode;
        if (string.IsNullOrEmpty(code))
            throw new NotFoundException($"Retrieved attendance code for meeting {meetingId} is null or empty");
        var retrievedUserId = meeting.Attendees.FirstOrDefault(x => x.UserId == userId)?.UserId;
        if (string.IsNullOrEmpty(retrievedUserId))
            throw new NotFoundException($"UserId: {userId} is not in the list of attendees for meeting: {meetingId}");
        var status = await _logic.SendMailToSingleUserAsync(code, userId, token);
        var response = new Response
        {
            Data = status,
            Message = status ? "Successfully sent code" : "Failed to send code" ,
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = status
        };
        return response;
    }

    public async Task<Response> NotifyAllToMarkAttendance(string meetingId, CancellationToken token)
    {
        var loggedInUser = GetLoggedUser();
        var meeting = await _unit.Meetings.GetMeeting_Attendees(meetingId, loggedInUser.CompanyId);
        if (meeting == null || meeting.ModelStatus == ModelStatus.Deleted) throw new NotFoundException($"Meeting with Id: {meetingId} not found");
        var code = meeting?.AttendanceGeneratedCode;
        if (string.IsNullOrEmpty(code))
            throw new NotFoundException($"Retrieved attendance code for meeting {meetingId} is null or empty");
        var userIds = meeting.Attendees.Where(x => x.IsPresent == false).Select(x => x.UserId).ToList();
        var status = await _logic.SendNotificationToBulkUser($"Kindly be reminded to mark your attendance for meeting: {meeting.Title}", userIds, token);
        var response = new Response
        {
            Data = status,
            Message = status ? "Successfully sent code" : "Failed to send code" ,
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = status
        };
        return response;
    }

    public async Task<Response> NotifyUserToMarkAttendance(string meetingId, string userId, CancellationToken token)
    {
        var loggedInUser = GetLoggedUser();
        var meeting = await _unit.Meetings.GetMeeting_Attendees(meetingId, loggedInUser.CompanyId);
        if (meeting == null || meeting.ModelStatus == ModelStatus.Deleted) throw new NotFoundException($"Meeting with Id: {meetingId} not found");
        var code = meeting?.AttendanceGeneratedCode;
        if (string.IsNullOrEmpty(code))
            throw new NotFoundException($"Retrieved attendance code for meeting {meetingId} is null or empty");
        var retrievedUserId = meeting.Attendees.FirstOrDefault(x => x.UserId == userId);
        var status = await _logic.SendNotificationToSingleUser($"Kindly be reminded to mark your attendance for meeting: {meeting.Title}", userId, token);
        var response = new Response
        {
            Data = status,
            Message = status ? "Successfully sent code" : "Failed to send code" ,
            StatusCode = HttpStatusCode.Created.ToString(),
            IsSuccessful = status
        };
        return response;
    }

    public async Task<Response> MarkAttendance(string meetingId, string userId, string inputtedAttendanceCode, CancellationToken token)
    {
        var user = GetLoggedUser();
        var meeting = await _unit.Meetings.GetMeeting_Attendees(meetingId, user.CompanyId);
        if (meeting == null || meeting.ModelStatus == ModelStatus.Deleted) throw new NotFoundException($"Meeting with Id: {meetingId} not found");
        var attendingUser = meeting.Attendees?.FirstOrDefault(x => x.UserId == userId);
        if (attendingUser == null)
            throw new NotFoundException($"User with Id: {userId} not found as an attendee of meeting: {meetingId}");
        var code = meeting.AttendanceGeneratedCode;
        var successful = false;
        var message = $"Could not mark attendee {userId} as present";
        if (code == inputtedAttendanceCode.ToUpper())
        {
            attendingUser.IsPresent = true;
            successful = true;
            _unit.SaveToDB();
        }
        else
        {
            message = $"Inputted code {inputtedAttendanceCode} is not correct";
            _logger.LogInformation($"Could not mark attendance for userId {userId}, expected code {code} is not same as input code: {inputtedAttendanceCode} ");
        }
        var response = new Response
        {
            Data = attendingUser.IsPresent,
            Message = !successful ? message : $"Attendee {userId} marked successfully" ,
            StatusCode = HttpStatusCode.OK.ToString(),
            IsSuccessful = successful
        };
        _logger.LogInformation("Create new meeting successful: {response}", response);
        
        return response;
    }

    public async Task<Response> GetAttendanceDetails(string meetingId, CancellationToken token)
    {
        var user = GetLoggedUser();
        var meeting = await _unit.Meetings.GetMeeting_Attendees(meetingId, user.CompanyId);
        if (meeting == null || meeting.ModelStatus == ModelStatus.Deleted) throw new NotFoundException($"Meeting with Id: {meetingId} not found");
        var attendingUser = meeting.Attendees?.Select(x =>
            new AttendanceDetails
            {
                UserId = x.UserId,
                IsPresent = x.IsPresent,
                AttendeePosition = x.AttendeePosition,
            }).ToList();
        var response = new Response
        {
            Data = attendingUser,
            Message = "Retrieved Attendance List" ,
            StatusCode = HttpStatusCode.OK.ToString(),
            IsSuccessful = true
        };
        return response;
    }

    class AttendanceDetails
    {
        public string UserId { get; set; }
        public bool IsPresent { get; set; }
        public string Role { get; set; }
        public AttendeePosition AttendeePosition { get; set; }
    }
    
    
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
}
