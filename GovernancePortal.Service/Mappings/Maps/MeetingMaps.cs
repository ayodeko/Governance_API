using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.Mappings.IMaps;

namespace GovernancePortal.Service.Mappings.Maps;


public class MeetingsAutoMapper : Profile
{
    public MeetingsAutoMapper()
    {
        CreateMap<MeetingPOST, Meeting>();
        CreateMap<Meeting, UpdateMeetingGET>();
        CreateMap<Meeting, MeetingListGET>();
        CreateMap<Meeting, UpdateAttendingUsersPOST>();
        CreateMap<Meeting, UpdateMeetingAgendaItemPOST>();
        CreateMap<MeetingAgendaItem, AgendaItemPOST>();
        CreateMap<AttendingUser, AttendingUserPOST>();
        CreateMap<NoticeMeeting, UpdateMeetingNoticePOST>();
        //CreateMap<MeetingModel, MeetingListGET>().ForMember(x => x.AttendanceId, option => option.MapFrom(y => y.Attendance.Id));
    }
}

public class MeetingMaps : IMeetingMaps
{
    private readonly IMapper _autoMapper;
    public MeetingMaps()
    {
        var profiles = new List<Profile>() { new MeetingsAutoMapper() };
        var mapperConfiguration = new MapperConfiguration(config => config.AddProfiles(profiles));
        _autoMapper = mapperConfiguration.CreateMapper();
    }
    public Meeting InMap(CreateMeetingPOST source,  Meeting destination) => new Meeting
    {
        Title = source.Title,
        Description = source.Description,
        Duration = source.Duration,
        DateTime = source.DateTime,
        Type = source.Type,
        Frequency = source.Frequency,
        Medium = source.Medium,
        
    };

    public MeetingListGET OutMap(Meeting existingMeeting, MeetingListGET updateMeetingAttendingUserGet) => _autoMapper.Map(existingMeeting, new MeetingListGET());
    public List<MeetingListGET> OutMap(List<Meeting> source) => source.Select(x => _autoMapper.Map(x, new MeetingListGET())).ToList();

    public List<UpdateMeetingPackItemPOST> OutMap(Meeting existingMeeting,
        List<UpdateMeetingPackItemPOST> updateMeetingAgendaItemPOST) =>
        existingMeeting.Packs.Select(x =>
        {
            var res = _autoMapper.Map(x, new UpdateMeetingPackItemPOST());
            res.Title = existingMeeting.Items.FirstOrDefault(y => y.Id == res.MeetingAgendaItemId)?.Title;
            return res;
        }).ToList();

    public UpdateMeetingGET OutMap(Meeting existingMeeting) => _autoMapper.Map(existingMeeting, new UpdateMeetingGET());

    public UpdateMeetingNoticePOST OutMap(NoticeMeeting existingNoticeMeeting,
        UpdateMeetingNoticePOST updateMeetingNoticePost) =>
        _autoMapper.Map(existingNoticeMeeting, updateMeetingNoticePost);

    public Meeting InMap(UpdateMeetingNoticePOST noticePost, Meeting existingMeeting)
    {
       var not = existingMeeting.Notice ?? new NoticeMeeting();
       not.MeetingId = existingMeeting.Id;
       not.Mandate = noticePost.Mandate;
       not.AgendaText = noticePost.AgendaText;
       not.Salutation = noticePost.Salutation;
       not.NoticeDate = noticePost.NoticeDate;
       not.NoticeText = noticePost.NoticeText;
       not.Signature = noticePost.Signature;
       not.Signatory = noticePost.Signatory;
       existingMeeting.Notice = not;
       return existingMeeting;
    }

    #region Attending User Maps

    public UpdateAttendingUsersPOST OutMap(Meeting existingMeeting, UpdateAttendingUsersPOST updateAttendingUsersPost) => _autoMapper.Map(existingMeeting, updateAttendingUsersPost);

    public UpdateMeetingAgendaItemPOST OutMap(Meeting existingMeeting,
        UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPost) =>
        _autoMapper.Map(existingMeeting, updateMeetingAgendaItemPost);

    public Meeting InMap(UpdateAttendingUsersPOST updateMeetingAttendeesPost, Meeting meeting)
    {
        meeting.Attendees = InMap(updateMeetingAttendeesPost.Attendees, meeting);
        return meeting;
    }

    public List<AttendingUser> InMap(List<AttendingUserPOST> updateMeetingAttendeesPost, Meeting meeting)
    {
        var attendingUsers = new List<AttendingUser>();
        foreach (var attendingUserPost in updateMeetingAttendeesPost)
        {
            var attendingUser = InMap(attendingUserPost, meeting);
            attendingUsers.Add(attendingUser);
        }

        return attendingUsers;
    }

    public AttendingUser InMap(AttendingUserPOST updateMeetingAttendeesPost, Meeting meeting)
    {
        AttendingUser result =  new()
        {
            UserId = updateMeetingAttendeesPost.UserId,
            Name = updateMeetingAttendeesPost.Name,
            AttendeePosition = updateMeetingAttendeesPost.AttendeePosition,
            IsGuest = updateMeetingAttendeesPost.IsGuest,
            IsPresent = updateMeetingAttendeesPost.IsPresent,
            MeetingId = meeting.Id,
            CompanyId = meeting.CompanyId,
        };
        result.Id = updateMeetingAttendeesPost.Id ?? result.Id;
        return result;
    }

    #endregion
    
    #region Agenda Items
    
    public Meeting InMap(UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPost, List<MeetingAgendaItem> agendaItems, Meeting meeting)
    {
        var editedMeeting = meeting;
        //editedMeeting.Items = agendaItems;
        meeting.Items = InMap(updateMeetingAgendaItemPost.Items, agendaItems, editedMeeting);
        return meeting;
    }

    public List<MeetingAgendaItem> InMap(List<AgendaItemPOST> updateMeetingAgendaItemPost, List<MeetingAgendaItem> agendaItems, Meeting meeting)
    {
        if (updateMeetingAgendaItemPost == null || !updateMeetingAgendaItemPost.Any()) return null;
        var newAgendaItemsList = new List<MeetingAgendaItem>();
        foreach (var agendaItemPost in updateMeetingAgendaItemPost)
        {
            var agendaItem = InMap(agendaItemPost,agendaItems, meeting);
            newAgendaItemsList.Add(agendaItem);
        }

        return newAgendaItemsList;
    }

    public MeetingAgendaItem InMap(AgendaItemPOST agendaItemPost, List<MeetingAgendaItem> agendaItems, Meeting meeting)
    {
        var retrievedItem = agendaItems?.FirstOrDefault(x => x.Id == agendaItemPost.Id );

        var agendaItem = retrievedItem ?? new MeetingAgendaItem();
        agendaItem.MeetIdHolder = meeting?.Id;
        agendaItem.CompanyId = meeting?.CompanyId;
        agendaItem.Title = agendaItemPost.Title;
        agendaItem.Number = agendaItemPost.Number;
        agendaItem.SubItems = InMap(agendaItemPost.SubItems, agendaItems, meeting);
        return agendaItem;
    }


    #endregion
    
    #region Meeting Pack Maps

    public Meeting InMap(UpdateMeetingPackPOST updateMeetingPackPost, Meeting existingMeeting)
    {
        existingMeeting.Packs = InMap(updateMeetingPackPost.Packs, existingMeeting);
        return existingMeeting;
    }

    private List<MeetingPackItem> InMap(List<UpdateMeetingPackItemPOST> meetingPacksPOST, Meeting existingMeeting)
    {
        var meetingPackItems = new List<MeetingPackItem>();
        foreach (var meetingPackPOST in meetingPacksPOST)
        {
            var meetingPackItem = InMap(meetingPackPOST, existingMeeting);
            meetingPackItems.Add(meetingPackItem);
        }

        return meetingPackItems;
    }

    private MeetingPackItem InMap(UpdateMeetingPackItemPOST updateMeetingPackPost, Meeting existingMeeting)
    {
        var meetingPackItem = new MeetingPackItem()
        {
            MeetingId = existingMeeting.Id,
            MeetingAgendaItemId = updateMeetingPackPost.MeetingAgendaItemId,
            PresenterUserId = updateMeetingPackPost.PresenterUserId,
            Description = updateMeetingPackPost.Description,
            Duration = updateMeetingPackPost.Duration,
            CoCreators = updateMeetingPackPost.CoCreators.Select(x => new MeetingPackItemUser
            {
                AttendingUserId = x
            }).ToList(),
            RestrictedUsers = updateMeetingPackPost.RestrictedUsers.Select(x => new MeetingPackItemUser
            {
                AttendingUserId = x
            }).ToList(),
            InterestTagUsers = updateMeetingPackPost.InterestTagUsers.Select(x => new MeetingPackItemUser
            {
                AttendingUserId = x
            }).ToList(),
        };
        return meetingPackItem;
    }

    #endregion

    #region Minutes Maps

    public Meeting InMap(UpdateMeetingMinutesPOST updateMinutesPost, Meeting existingMeeting)
    {
        return existingMeeting;
    }

    #endregion
}
