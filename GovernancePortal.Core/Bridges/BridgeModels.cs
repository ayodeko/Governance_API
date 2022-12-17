using System;

namespace GovernancePortal.Core.Bridges;

public class Meeting_Resolution
{
    public Meeting_Resolution()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }
    public string CompanyId { get; set; }
    public string MeetingId { get; set; }
    public string ResolutionId { get; set; }
}