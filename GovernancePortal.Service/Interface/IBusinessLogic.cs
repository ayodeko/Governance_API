using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GovernancePortal.Service.Interface;

public interface IBusinessLogic
{
    Task<bool> SendNotificationToSingleUser(string notificationMessage, string userId, CancellationToken token);
    Task<bool> SendNotificationToBulkUser(string notificationMessage, List<string> userIds, CancellationToken token);
    Task<bool> SendMailToSingleUserAsync(string notificationMessage, string userId, CancellationToken token);
    Task<bool> SendBulkMailByUserIdsAsync(string mailSubject, string notificationMessage, List<string> userIds, CancellationToken token);
}