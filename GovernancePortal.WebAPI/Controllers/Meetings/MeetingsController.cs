using System;
using System.Net;
using System.Threading.Tasks;
using GovernancePortal.Core.General;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.Interface;
using GovernancePortal.WebAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GovernancePortal.WebAPI.Controllers.Meetings
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        private readonly IExceptionHandler _exceptionHandler;

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
                var newMeeting = await _meetingService.CreateMeeting(loggedUser, meeting);
                return Ok(
                    new Response
                    {
                        Data = newMeeting,
                        Message = "Meeting created successfully",
                        StatusCode = HttpStatusCode.Created.ToString(),
                        IsSuccessful = true
                    });
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
                var meetingModel = await _meetingService.UpdateMeeting(meetingId, loggedUser, meeting);
                return Ok(
                    new Response
                    {
                        Data = meetingModel,
                        Message = "Meeting created successfully",
                        StatusCode = HttpStatusCode.Created.ToString(),
                        IsSuccessful = true
                    });
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