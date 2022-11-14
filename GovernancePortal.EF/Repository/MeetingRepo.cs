using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace GovernancePortal.EF.Repository;


public class MeetingRepo : GenericRepo<Meeting>, IMeetingRepo
{
    public MeetingRepo(DbContext context) : base(context)
    {
    }

    public async Task<Meeting> GetMeeting(string meetingId, string companyId)
    {
        return (await _context.Set<Meeting>()
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId)))!;
    }
    public async Task<Meeting> GetMeeting_AllDependencies(string meetingId, string companyId)
    {
        return (await _context.Set<Meeting>()
            .Include(x => x.Attendees)
            .Include(x => x.Items)
            .Include(x => x.Packs)
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId)))!;
    }
    public async Task<Meeting> GetMeeting_Attendees(string meetingId, string companyId)
    {
        return (await _context.Set<Meeting>()
            .Include(x => x.Attendees)
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId)))!;
    }
    public async Task<Meeting> GetMeeting_AgendaItems(string meetingId, string companyId)
    {
        return await _context.Set<Meeting>()
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId));
    }

    public async Task<Meeting> GetMeeting_AgendaItems_Attendees_Notice(string meetingId, string companyId)
    {
        return await _context.Set<Meeting>()
            .Include(x => x.Items)
            .Include(y => y.Attendees)
            .Include(z => z.Notice)
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId));
    }

    public async Task<Meeting> GetMeeting_AgendaItems_MeetingPack(string meetingId, string companyId)
    {
        return (await _context.Set<Meeting>()
            .Include(x => x.Items)
            .Include(x => x.Packs)
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId)))!;
    }

    public IEnumerable<Meeting> GetMeetingListByUserId(string userId, string companyId, int pageNumber, int pageSize,
        out int totalRecords)
    {
        var skip = (pageNumber - 1) * pageSize;
        var result = (_context.Set<Meeting>()
            .Include(x => x.Attendees)
            .Where(x => x.CompanyId.Equals(companyId) && x.Attendees.Exists(c => c.UserId == userId)));
        totalRecords = result.Count();
        return result.Skip(skip)
            .Take(pageSize)!;
    }
    
    public IEnumerable<Meeting> FindBySearchString(string searchString, string companyId)
    {
        return _context.Set<Meeting>().
            Include(x => x.Attendees).
            Where(x => x.CompanyId == companyId && x.Title.Contains(searchString));
    }
    public IEnumerable<Meeting> FindByDate(DateTime dateTime, string companyId)
    {
        return _context.Set<Meeting>().
            Include(x => x.Attendees).
            Where(x => x.CompanyId == companyId && x.DateTime == dateTime);
    }
}