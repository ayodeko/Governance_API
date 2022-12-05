using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovernancePortal.Service.ClientModels.General;

namespace GovernancePortal.Service.Interface;

public interface IAttendanceServices
{
    Task<Response> GenerateAttendanceCode(string meetingId, CancellationToken token);
    Task<Response> RetrieveGeneratedAttendanceCode(string meetingId, CancellationToken token);
    Task<Response> SendAttendanceCodeToAll(string meetingId, CancellationToken token);
    Task<Response> SendAttendanceCodeToUser(string meetingId, string userId, CancellationToken token);
    Task<Response> NotifyAllToMarkAttendance(string meetingId, CancellationToken token);
    Task<Response> NotifyUserToMarkAttendance(string meetingId, string userId, CancellationToken token);
    Task<Response> MarkAttendance(string meetingId, string userId, string inputtedAttendanceCode, CancellationToken token);
    Task<Response> GetAttendanceDetails(string meetingId, CancellationToken token);
}


