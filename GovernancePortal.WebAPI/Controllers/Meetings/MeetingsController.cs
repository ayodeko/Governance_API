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
        private readonly IMeetingService _meetingService;
        private readonly IExceptionHandler _exceptionHandler;
        private readonly ILogger _logger;

        public MeetingsController(IMeetingService meetingService, IExceptionHandler exceptionHandler)
        {
            _meetingService = meetingService;
            _exceptionHandler = exceptionHandler;
        }
        [HttpPost("Meeting/Create")]
        public async Task<ActionResult<Response>> CreateMeeting(CreateMeetingPOST meeting)
        {
            try
            {
                Person loggedUser = GetLoggedUser();
                _logger.LogInformation("Inside Create New Meeting");
                var newMeeting = await _meetingService.CreateMeeting(loggedUser, meeting);
                var response = new Response
                {
                    Data = newMeeting,
                    Message = "Meeting created successfully",
                    StatusCode = HttpStatusCode.Created.ToString(),
                    IsSuccessful = true
                };
                _logger.LogInformation("Create new meeting successful: {response}", response);
                return Ok(response);
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
                Person loggedUser = GetLoggedUser();
                _logger.LogInformation("Inside Update Meeting, {ID}", meetingId);
                var meetingModel = await _meetingService.UpdateMeeting(meetingId, loggedUser, meeting);
                var response = new Response
                {
                    Data = meetingModel,
                    Message = "Meeting Updated successfully",
                    StatusCode = HttpStatusCode.Created.ToString(),
                    IsSuccessful = true
                };
                _logger.LogInformation("Updated meeting {ID} successful: {response}", meetingId, response);
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
                Person loggedUser = GetLoggedUser();
                var newMeeting = await _meetingService.AddPastMeeting(loggedUser, meeting);
                var response = new Response
                {
                    Data = newMeeting,
                    Message = "Past meeting added successfully",
                    StatusCode = HttpStatusCode.Created.ToString(),
                    IsSuccessful = true
                };
                _logger.LogInformation("Added past meeting successfully {response}", response);
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
                Person loggedUser = GetLoggedUser();
                _logger.LogInformation("Inside AddPastMinutes");
                var newMinutes = await _meetingService.AddPastMinutes(loggedUser, meeting);
                var response = new Response
                {
                    Data = newMinutes,
                    Message = "Past meeting added successfully",
                    StatusCode = HttpStatusCode.Created.ToString(),
                    IsSuccessful = true
                };
                _logger.LogInformation("Added past minutes successfully {response}", response);
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
            var loggedUser = GetLoggedUser();
            _logger.LogInformation("Inside get all meetings, {pageQuery}", pageQuery);
            try
            {
                var pagedIndex = await _meetingService.GetAllMeetings(loggedUser.CompanyId, pageQuery);
                
                return Ok(pagedIndex);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.GetResponse(ex);
            }
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
    }
}