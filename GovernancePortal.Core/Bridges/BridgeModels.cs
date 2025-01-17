﻿using System;

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
    public ResolutionType ResolutionType { get; set; }
}
public class Task_Resolution
{
    public Task_Resolution()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }
    public string CompanyId { get; set; }
    public string TaskId { get; set; }
    public string ResolutionId { get; set; }
    public ResolutionType ResolutionType { get; set; }
}

public enum ResolutionType
{
    Vote,
    Poll
}

public class Meeting_Task
{
    public Meeting_Task()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }
    public string CompanyId { get; set; }
    public string MeetingId { get; set; }
    public string TaskId { get; set; }
}