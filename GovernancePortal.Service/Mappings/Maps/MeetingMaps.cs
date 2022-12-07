using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Runtime;
using AutoMapper;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.ClientModels.Meetings.Minute;
using GovernancePortal.Service.Mappings.IMaps;

namespace GovernancePortal.Service.Mappings.Maps;

public class MeetingsAutoMapper : Profile
{
    public MeetingsAutoMapper()
    {
        CreateMap<MeetingPOST, Meeting>();
        CreateMap<Meeting, UpdateMeetingGET>();
        CreateMap<Meeting, MeetingGET>();
        CreateMap<Meeting, MeetingListGET>();
        CreateMap<Meeting, UpdateAttendingUsersPOST>();
        CreateMap<Meeting, UpdateMeetingAgendaItemPOST>();
        CreateMap<Meeting, FullUpdateMeetingAgendaItemPOST>();
        CreateMap<MeetingAgendaItem, AgendaItemPOST>();
        CreateMap<MeetingAgendaItem, FullAgendaItemPOST>();
        CreateMap<MeetingAgendaItem, SubAgendaItemPOST>();
        CreateMap<AttendingUser, AttendingUserPOST>();
        CreateMap<NoticeMeeting, UpdateMeetingNoticePOST>();
        CreateMap<MeetingPackItem, UpdateMeetingPackItemPOST>();
        CreateMap<MeetingPackItemUser, MeetingPackUserPOST>();
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

    #region meeting
    
    public Meeting InMap(CreateMeetingPOST source,  Meeting destination) => new Meeting
    {
        Title = source.Title,
        Description = source.Description,
        Venue = source.Venue,
        Link = source.Link,
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
            res.Title = existingMeeting.Items?.FirstOrDefault(y => y.Id == res.MeetingAgendaItemId)?.Title;
            return res;
        }).ToList();

    public MeetingGET OutMap(Meeting existingMeeting, MeetingGET meetingGet) => _autoMapper.Map(existingMeeting, meetingGet);

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
    #endregion
   
    #region Attending User Maps

    public UpdateAttendingUsersPOST OutMap(Meeting existingMeeting, UpdateAttendingUsersPOST updateAttendingUsersPost) => _autoMapper.Map(existingMeeting, updateAttendingUsersPost);

    public UpdateMeetingAgendaItemPOST OutMap(Meeting existingMeeting,
        UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPost) =>
        _autoMapper.Map(existingMeeting, updateMeetingAgendaItemPost);
    public FullUpdateMeetingAgendaItemPOST OutMap(Meeting existingMeeting,
        FullUpdateMeetingAgendaItemPOST updateMeetingAgendaItemPost) =>
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
        var res = meeting.Attendees.FirstOrDefault(x => x.Id == updateMeetingAttendeesPost.Id);
        var result = res ?? new AttendingUser();
        result.UserId = updateMeetingAttendeesPost.UserId;
        result.Name = updateMeetingAttendeesPost.Name;
        result.AttendeePosition = updateMeetingAttendeesPost.AttendeePosition;
        result.IsGuest = updateMeetingAttendeesPost.IsGuest;
        result.MeetingId = meeting.Id;
        result.CompanyId = meeting.CompanyId;
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

        //why is this here?
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

    #region Full Agenda Items Maps

    public Meeting InMap(FullUpdateMeetingAgendaItemPOST updateMeetingAgendaItemPost, List<MeetingAgendaItem> agendaItems, Meeting meeting)
    {
        var editedMeeting = meeting;
        //editedMeeting.Items = agendaItems;
        meeting.Items = InMap(updateMeetingAgendaItemPost.Items, agendaItems, editedMeeting);
        return meeting;
    }
    
    public List<MeetingAgendaItem> InMap(List<FullAgendaItemPOST> updateMeetingAgendaItemPost, List<MeetingAgendaItem> agendaItems, Meeting meeting)
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
    public List<MeetingAgendaItem> InMap(List<SubAgendaItemPOST> updateMeetingAgendaItemPost, List<MeetingAgendaItem> agendaItems, Meeting meeting)
    {
        if (updateMeetingAgendaItemPost == null || !updateMeetingAgendaItemPost.Any()) return null;
        var newAgendaItemsList = new List<MeetingAgendaItem>();
        foreach (var agendaItemPost in updateMeetingAgendaItemPost)
        {
            var agendaItem = InMapSubItems(agendaItemPost,agendaItems, meeting);
            newAgendaItemsList.Add(agendaItem);
        }
        return newAgendaItemsList;
    }
    
    public MeetingAgendaItem InMap(FullAgendaItemPOST agendaItemPost, List<MeetingAgendaItem> agendaItems, Meeting meeting)
    {
        var retrievedItem = agendaItems?.FirstOrDefault(x => x.Id == agendaItemPost.Id );

        var agendaItem = retrievedItem ?? new MeetingAgendaItem();
        agendaItem.MeetIdHolder = meeting?.Id;
        agendaItem.CompanyId = meeting?.CompanyId;
        agendaItem.Title = agendaItemPost.Title;
        agendaItem.Number = agendaItemPost.Number;
        agendaItem.PresenterUserId = agendaItemPost.PresenterUserId;
        agendaItem.Description = agendaItemPost.Description;
        agendaItem.Duration = agendaItemPost.Duration;
        agendaItem.CoCreators = agendaItemPost.CoCreators
            ?.Where(x => !string.IsNullOrEmpty(x.UserId)).Select(x =>
            {
                var retrievedUser = agendaItem?.CoCreators?.FirstOrDefault(y =>
                    y.CoCreatorId == agendaItem.Id && y.UserId == x.UserId);
                var packUser = retrievedUser ?? new MeetingPackItemUser();
                packUser.UserId = x.UserId;
                packUser.AgendaItemId = agendaItem.Id;
                packUser.CoCreatorId = agendaItem.Id;
                packUser.MeetingIdHolder = meeting?.Id;
                packUser.CompanyId = meeting?.CompanyId;
                return packUser;

            }).ToList();
        agendaItem.RestrictedUsers = agendaItemPost.RestrictedUsers
            ?.Where(x => !string.IsNullOrEmpty(x.UserId)).Select(x =>
            {
                var retrievedUser = agendaItem?.RestrictedUsers?.FirstOrDefault(y =>
                    y.RestrictedUserId == agendaItem.Id && y.UserId == x.UserId);
                var packUser = retrievedUser ?? new MeetingPackItemUser();
                packUser.UserId = x.UserId;
                packUser.AgendaItemId = agendaItem.Id;
                packUser.RestrictedUserId = agendaItem.Id;
                packUser.MeetingIdHolder = meeting?.Id;
                packUser.CompanyId = meeting?.CompanyId;
                return packUser;

            }).ToList();
        agendaItem.InterestTagUsers = agendaItemPost.InterestTagUsers
            ?.Where(x => !string.IsNullOrEmpty(x.UserId)).Select(x =>
            {
                var retrievedUser = agendaItem?.InterestTagUsers?.FirstOrDefault(y =>
                    y.InterestTagUserId == agendaItem.Id && y.UserId == x.UserId);
                var packUser = retrievedUser ?? new MeetingPackItemUser();
                packUser.UserId = x.UserId;
                packUser.AgendaItemId = agendaItem.Id;
                packUser.InterestTagUserId = agendaItem.Id;
                packUser.MeetingIdHolder = meeting?.Id;
                packUser.CompanyId = meeting?.CompanyId;
                return packUser;
            }).ToList();
        agendaItem.SubItems = InMap(agendaItemPost.SubItems, agendaItems, meeting);
        return agendaItem;
    }
    
    public MeetingAgendaItem InMapSubItems(SubAgendaItemPOST agendaItemPost, List<MeetingAgendaItem> agendaItems, Meeting meeting)
    {
        var retrievedItem = agendaItems?.FirstOrDefault(x => x.Id == agendaItemPost.Id );

        var agendaItem = retrievedItem ?? new MeetingAgendaItem();
        agendaItem.MeetIdHolder = meeting?.Id;
        agendaItem.CompanyId = meeting?.CompanyId;
        agendaItem.Title = agendaItemPost.Title;
        agendaItem.Number = agendaItemPost.Number;
        agendaItem.PresenterUserId = agendaItemPost.PresenterUserId;
        agendaItem.Description = agendaItemPost.Description;
        agendaItem.Duration = agendaItemPost.Duration;
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

    private MeetingPackItem InMap(UpdateMeetingPackItemPOST packPost, Meeting existingMeeting)
    {
        var retrievedPack = existingMeeting.Packs?.FirstOrDefault(x => x.Id == packPost.Id );
        var meetingPackItem = retrievedPack ?? new MeetingPackItem();
        meetingPackItem.MeetingId = existingMeeting.Id;
        meetingPackItem.MeetingAgendaItemId = packPost.MeetingAgendaItemId;
        meetingPackItem.PresenterUserId = packPost.PresenterUserId;
        meetingPackItem.Description = packPost.Description;
       /* meetingPackItem.Duration = packPost.Duration;
        meetingPackItem.CoCreators = packPost.CoCreators
            ?.Where(x => !string.IsNullOrEmpty(x.UserId)).Select(x =>
            {
                var retrievedUser = retrievedPack?.CoCreators?.FirstOrDefault(y =>
                    y.CoCreatorId == meetingPackItem.Id && y.UserId == x.UserId);
                var packUser = retrievedUser ?? new MeetingPackItemUser();
                packUser.UserId = x.UserId;
                packUser.AgendaItemId = packPost.MeetingAgendaItemId;
                packUser.CoCreatorId = meetingPackItem.Id;
                return packUser;

            }).ToList();
        meetingPackItem.RestrictedUsers = packPost.RestrictedUsers
            ?.Where(x => !string.IsNullOrEmpty(x.UserId)).Select(x =>
            {
                var retrievedUser = retrievedPack?.RestrictedUsers?.FirstOrDefault(y =>
                    y.RestrictedUserId == meetingPackItem.Id && y.UserId == x.UserId);
                var packUser = retrievedUser ?? new MeetingPackItemUser();
                packUser.UserId = x.UserId;
                packUser.AgendaItemId = packPost.MeetingAgendaItemId;
                packUser.RestrictedUserId = meetingPackItem.Id;
                return packUser;

            }).ToList();
        meetingPackItem.InterestTagUsers = packPost.InterestTagUsers
            ?.Where(x => !string.IsNullOrEmpty(x.UserId)).Select(x =>
            {
                var retrievedUser = retrievedPack?.InterestTagUsers?.FirstOrDefault(y =>
                    y.InterestTagUserId == meetingPackItem.Id && y.UserId == x.UserId);
                var packUser = retrievedUser ?? new MeetingPackItemUser();
                packUser.UserId = x.UserId;
                packUser.AgendaItemId = packPost.MeetingAgendaItemId;
                packUser.InterestTagUserId = meetingPackItem.Id;
                return packUser;
            }).ToList();*/
        return meetingPackItem;
    }
    
    #endregion

    #region Minutes Maps

    public Meeting InMap(UpdateMeetingMinutesPOST updateMinutesPost, Meeting existingMeeting)
    {
        return existingMeeting;
    }

    public Meeting InMap(List<AddMinutePOST> source, Meeting existingMeeting)
    {
        existingMeeting.Minutes = InMap(source, existingMeeting, new List<Minute>());
        return existingMeeting;
    }
    public List<Minute> InMap(List<AddMinutePOST> source,Meeting existingMeeting, List<Minute> destination = null)
    {
        var returnModel = new List<Minute>();
        
        foreach(var item in source)
        {
            returnModel.Add(InMap(item, existingMeeting, new Minute()));
        }
        return returnModel;
    }

    public Minute InMap(AddMinutePOST source, Meeting existingMeeting, Minute destination = null)
    {
        if (destination is null)
            destination = new Minute();

        destination.MinuteText = source.MinuteText;
        destination.MeetingId = existingMeeting.Id;
        destination.CompanyId = existingMeeting.CompanyId;
        destination.AgendaItemId = source.AgendaItemId;
        return destination;

    }

    #endregion
}
