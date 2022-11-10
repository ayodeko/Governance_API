using System;
using System.Net;
using System.Threading.Tasks;
using GovernancePortal.Core.General;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.Interface;
using GovernancePortal.WebAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GovernancePortal.WebAPI.Controllers.Meetings
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingService_depr _meetingServiceDepr;
        private readonly IExceptionHandler _exceptionHandler;

        public MeetingsController(IMeetingService_depr meetingServiceDepr, IExceptionHandler exceptionHandler)
        {
            _meetingServiceDepr = meetingServiceDepr;
            _exceptionHandler = exceptionHandler;
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
        [HttpPost("Meeting/Create")]
        public async Task<ActionResult<Response>> CreateMeeting(CreateMeetingPOST meeting)
        {
            try
            {
                var response = await _meetingServiceDepr.CreateMeeting(meeting);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.GetResponse(ex);
            }
        }
        
        
        [HttpGet("List")]
        public async Task<ActionResult<Response>> GetAllMeetings([FromQuery] PageQuery pageQuery)
        {
            try
            {
                var pagedIndex = await _meetingServiceDepr.GetAllMeetings(pageQuery);
                return Ok(pagedIndex);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.GetResponse(ex);
            }
        }
        
        [HttpPost("{meetingId}/Meeting/Update")]
        public async Task<ActionResult<Response>> UpdateMeeting(string meetingId, UpdateMeetingPOST meeting)
        {
            try
            {
                var response = await _meetingServiceDepr.UpdateMeeting(meetingId, meeting);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.GetResponse(ex);
            }
        }
        
        [HttpPost("Meeting/AddPastMeeting")]
        public async Task<ActionResult<Response>> AddPastMeeting(AddPastMeetingPOST meeting)
        {
            try
            {
                var response = await _meetingServiceDepr.AddPastMeeting(meeting);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.GetResponse(ex);
            }
        }
        
        [HttpPost("Meeting/AddPastMinutes")]
        public async Task<ActionResult<Response>> AddPastMinutes(AddPastMinutesPOST meeting)
        {
            try
            {
                var response = await _meetingServiceDepr.AddPastMinutes(meeting);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.GetResponse(ex);
            }
        }
        
        [HttpPost("Meeting/AddPastAttendance")]
        public async Task<ActionResult<Response>> AddPastAttendance(AddPastAttendancePOST meetingAttendance)
        {
            try
            {
                return await _meetingServiceDepr.AddPastAttendance(meetingAttendance);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.GetResponse(ex);
            }
        }
        [HttpPost("{Id}")]
        public async Task<ActionResult<Response>> GetMeetingById(string Id)
        {
            try
            {
                return await _meetingServiceDepr.GetMeetingById(Id);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.GetResponse(ex);
            }
        }
        
        
    }
}