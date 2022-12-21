using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
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

    public async Task<AttendingUser> GetAttendingUsers(string meetingId, string companyId, CancellationToken token)
    {
        return (await _context.Set<AttendingUser>()
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId), token))!;
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
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId)))!;
    }
    public async Task<Meeting> GetMeeting_AgendaItems(string meetingId, string companyId)
    {
        /*return new Meeting()
        {
            Items = _context.Set<MeetingAgendaItem>().Include(x => x.SubItems)
                .Where(x => x.MeetingId == meetingId).ToList()
        };*/
        return await _context.Set<Meeting>()
            .Include(x => x.Items)
            .ThenInclude(x => x.SubItems)
            .ThenInclude(x => x.SubItems)
            .ThenInclude(x => x.SubItems)
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId));
    }
    public async Task<Meeting> GetMeeting_AgendaItems_Relationships(string meetingId, string companyId)
    {
        /*return new Meeting()
        {
            Items = _context.Set<MeetingAgendaItem>().Include(x => x.SubItems)
                .Where(x => x.MeetingId == meetingId).ToList()
        };*/
        return await _context.Set<Meeting>()
            .Include(x => x.Items)
            .ThenInclude(x => x.SubItems)
            .ThenInclude(x => x.SubItems)
            .ThenInclude(x => x.SubItems)
            .Include(x => x.Items)
            .ThenInclude(x => x.CoCreators)
            .Include(x => x.Items)
            .ThenInclude(x => x.RestrictedUsers)
            .Include(x => x.Items)
            .ThenInclude(x => x.InterestTagUsers)
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId));
    }
    
    
    public IEnumerable<MeetingAgendaItem> GetAgendaItems_With_MeetingHolder(string meetingId, string companyId)
    {
        return  _context.Set<MeetingAgendaItem>()
            .Include(x => x.SubItems)
            .ThenInclude(x => x.SubItems)
            .ThenInclude(x => x.SubItems)
            .Where(x => x.MeetIdHolder.Equals(meetingId) && x.CompanyId.Equals(companyId));
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
            /*.ThenInclude(x => x.CoCreators)
            .Include(x => x.Packs)
            .ThenInclude(x => x.RestrictedUsers)
            .Include(x => x.Packs)
            .ThenInclude(x => x.InterestTagUsers)*/
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId)))!;
    }

    public async Task<Meeting> GetMeeting_MeetingPack(string meetingId, string companyId)
    {
        return (await _context.Set<Meeting>()
            .Include(x => x.Packs)
            /*.ThenInclude(x => x.CoCreators)
            .Include(x => x.Packs)
            .ThenInclude(x => x.RestrictedUsers)
            .Include(x => x.Packs)
            .ThenInclude(x => x.InterestTagUsers)*/
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId)))!;
    }
    public async Task<Meeting> GetMeeting_AgendaItems_Attendees_MeetingPack(string meetingId, string companyId)
    {
        return (await _context.Set<Meeting>()
            .Include(x => x.Items)
            .Include(x => x.Packs)
            .Include(x => x.Attendees)
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId)))!;
    }
    public async Task<Meeting> GetMeeting_Minutes(string meetingId, string companyId)
    {
        return (await _context.Set<Meeting>()
            .Include(x => x.Minutes).ThenInclude(x=>x.AgendaItem)
            .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId)))!;
    }

    public IEnumerable<Meeting> GetMeetingListByMeetingType(MeetingType type, string companyId, int pageNumber, int pageSize,
        out int totalRecords)
    {
        var skip = (pageNumber - 1) * pageSize;
        var result = (_context.Set<Meeting>()
            .Include(x => x.Attendees)
            .Where(x => x.CompanyId.Equals(companyId) && x.Type == type));
        totalRecords = result.Count();
        return result.Skip(skip)
            .Take(pageSize)!;
    }
    public IEnumerable<Meeting> GetMeetingListByMeetingTypeAndUserId(MeetingType type, string userId, string companyId, int pageNumber, int pageSize,
        out int totalRecords)
    {
        var skip = (pageNumber - 1) * pageSize;
        var result = (_context.Set<Meeting>()
            .Include(x => x.Attendees)
            .Where(x => x.CompanyId.Equals(companyId) && x.Type == type && x.Attendees.Any(c => c.UserId == userId)));
        totalRecords = result.Count();
        return result.Skip(skip)
            .Take(pageSize)!;
    }
    public IEnumerable<Meeting> GetMeetingListByUserId(string userId, string companyId, int pageNumber, int pageSize,
        out int totalRecords)
    {
        var skip = (pageNumber - 1) * pageSize;
        var result = (_context.Set<Meeting>()
            .Include(x => x.Attendees)
            .Where(x => x.CompanyId.Equals(companyId) && x.Attendees.Any(c => c.UserId == userId)));
        totalRecords = result.Count();
        return result.Skip(skip)
            .Take(pageSize)!;
    }
    public IEnumerable<Meeting> GetMeetingList(string companyId, int pageNumber, int pageSize,
        out int totalRecords)
    {
        var skip = (pageNumber - 1) * pageSize;
        var result = (_context.Set<Meeting>()
            .Include(x => x.Attendees)
            .Where(x => x.CompanyId.Equals(companyId)));
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