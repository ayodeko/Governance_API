using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GovernancePortal.Service.ClientModels.Exceptions;
using GovernancePortal.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GovernancePortal.Service.Implementation;

public class BusinessLogicService : IBusinessLogic
{
    private IConfiguration Configuration;
    private ILogger _logger;

    public BusinessLogicService(IConfiguration _Configuration, ILogger logger)
    {
        Configuration = _Configuration;
        _logger = logger;
    }
    public Task<bool> SendNotificationToSingleUser(string notificationMessage, string redirectUrl, string userId, CancellationToken token = default)
    {
        return Task.FromResult<bool>(default);
    }

    public Task<bool> SendNotificationToBulkUser(string notificationMessage, string redirectUrl,  List<string> userIds, CancellationToken token = default)
    {
        return Task.FromResult<bool>(default);
    }

    public Task<bool> SendMailToSingleUserAsync(string notificationMessage, string userId, CancellationToken token = default)
    {
        return Task.FromResult<bool>(default);
    }

    public async Task<bool> SendBulkMailByUserIdsAsync(string subject, string message, List<string> userIds,
        CancellationToken token = default)
    {
        _logger.LogInformation("About to send bulk mails to userIds");
        var getEmailsUrl = Configuration?.GetSection("ExternalURLs")?["GetEmailByIdUrl"];
        string userIdsString = JsonConvert.SerializeObject(new {userIds});
        _logger.LogInformation("UserIds for email retrieval: {userIds}", JsonConvert.SerializeObject(userIdsString));
        var responseString = await StaticLogics.HttpPostAsync(getEmailsUrl, userIdsString, token);
        var responseBody = JsonConvert.DeserializeAnonymousType(responseString,
            new { status = "", data = new { emails = new List<string>() } });
        var emails = responseBody?.data?.emails;
        if (emails == null || !emails.Any())
        {
            throw new NotFoundException("Could not retrieve corresponding emails from user ids passed");
        }
        _logger.LogInformation("Emails for sending notifications retrieved");

        return await SendBulkMailByEmailAsync(subject, message, emails, token);
    }

    public async Task<bool> SendBulkMailByEmailAsync(string subject, string message, List<string> emails,
        CancellationToken Token = default)
    {
        var sendEmailsUrl = Configuration?.GetSection("ExternalURLs")?["SendEmailsUrl"];
        _logger.LogInformation($"Inside send email to users by email");
        var mailBody = new
        {
            receivers = emails,
            multipleReceipients = true,
            subject,
            htmlBody = message
        };
        var mailBodyString = JsonConvert.SerializeObject(mailBody);
        var responseString = await StaticLogics.HttpPostAsync(sendEmailsUrl, mailBodyString, Token);
        _logger.LogInformation($"Response string from SendBulkMailByEmailAsync: {responseString}");
        var response =
            JsonConvert.DeserializeAnonymousType(responseString, new { status = true, data = false, message = "" });
        return response?.data ?? false;
    }
}

public static class StaticLogics
{
    private static IConfiguration _configuration;

    public static void Init(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public static async Task<string> HttpGetAsync(string url, CancellationToken token = default)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(url, token);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync(token);
        return responseString;
    }
    public static async Task<string> HttpPostAsync(string url, string requestBody, CancellationToken token = default)
    {
        using var httpClient = new HttpClient();
        var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, content, token);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync(token);
        return responseString;
    }

    public static string DummyGetCurrentEnterpriseUser()
    {
        return _configuration?.GetSection("DummyData")?["CurrentUser"];
    }
    
}