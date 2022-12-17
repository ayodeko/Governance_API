using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovernancePortal.Service.Interface;

namespace GovernancePortal.Service.Implementation;

public class BusinessLogicService : IBusinessLogic
{
    public Task<bool> SendNotificationToSingleUser(string notificationMessage, string userId, CancellationToken token)
    {
        return Task.FromResult<bool>(default);
    }

    public Task<bool> SendNotificationToBulkUser(string notificationMessage, List<string> userIds, CancellationToken token)
    {
        return Task.FromResult<bool>(default);
    }

    public Task<bool> SendMailToSingleUserAsync(string notificationMessage, string userId, CancellationToken token)
    {
        return Task.FromResult<bool>(default);
    }

    public Task<bool> SendMailToBulkUsersAsync(string notificationMessage, List<string> userIds, CancellationToken token)
    {
        return Task.FromResult(false);
    }
}