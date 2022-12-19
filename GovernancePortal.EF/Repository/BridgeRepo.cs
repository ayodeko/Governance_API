﻿using System;
using System.Linq;
using System.Threading.Tasks;
using GovernancePortal.Core.Bridges;
using GovernancePortal.Core.Meetings;
using Microsoft.EntityFrameworkCore;

namespace GovernancePortal.EF.Repository;

public interface IBridgeRepo
{
    Task<Meeting_Resolution> RetrieveMeeting_Resolution(string meetingId, string resolutionId);
    Task AddMeeting_Resolution(string meetingId, string resolutionId, string companyId);
    Task<Meeting_Resolution> RetrieveMeetingByResolutionId(string resolutionId, string companyId);
}

public class BridgeRepo : IBridgeRepo
{
    private readonly PortalContext _context;

    public BridgeRepo(PortalContext context)
    {
        _context = context;
    }

    public async Task<Meeting_Resolution> RetrieveMeeting_Resolution(string meetingId, string resolutionId)
    {
        return await _context.Set<Meeting_Resolution>()
            .FirstOrDefaultAsync(x => x.MeetingId == meetingId && x.ResolutionId == resolutionId);
    }
    public async Task<Meeting_Resolution> RetrieveMeetingByResolutionId(string resolutionId, string companyId)
    {
        return await _context.Set<Meeting_Resolution>()
            .FirstOrDefaultAsync(x => x.ResolutionId == resolutionId && x.CompanyId == companyId);
    }
    
    public async Task AddMeeting_Resolution(string meetingId, string resolutionId, string companyId)
    {
        var retrievedMeeting_Resolution = await _context.Set<Meeting_Resolution>()
            .FirstOrDefaultAsync(x => x.MeetingId == meetingId && x.ResolutionId == resolutionId);
        if (retrievedMeeting_Resolution == null)
        {
            await _context.Set<Meeting_Resolution>().AddAsync(new Meeting_Resolution()
            {
                MeetingId = meetingId,
                ResolutionId = resolutionId,
                CompanyId = companyId
            });
        }
        else
        {
            throw new Exception(
                $"Bridge connection already of meetingId: {meetingId} and resolutionId: {resolutionId} already exists");
        }
    }
    
}