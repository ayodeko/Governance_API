using Microsoft.AspNetCore.Mvc;

namespace GovernancePortal.WebAPI.Controllers.Meetings
{
    public class MeetingPackController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok();
        }
    }
}