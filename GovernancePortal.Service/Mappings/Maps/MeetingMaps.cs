﻿using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Runtime;
using AutoMapper;
using GovernancePortal.Core.General;
using GovernancePortal.Core.Meetings;
using GovernancePortal.EF.Migrations;
using GovernancePortal.Service.ClientModels.Exceptions;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.ClientModels.Meetings.Minute;
using GovernancePortal.Service.ClientModels.TaskManagement;
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
        CreateMap<Minute, MinuteGET>();
        CreateMap<Minute, UploadMinutePOST>();
        CreateMap<Minute, UpdateMeetingMinutesGET>();
        //CreateMap<MeetingAgendaItem, BaseAgendaItemGET>();  
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
        IsPast = source.IsPast,
        MinutesStatus = source.MinutesStatus,
        NoticesStatus = source.NoticesStatus, 
        Status = source.Status
        
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

    public List<UpdateMeetingMinutesGET> OutMap(Meeting existingMeeting,
        List<UpdateMeetingMinutesGET> updateMeetingNoticePosts) =>
        existingMeeting.Minutes.Select(x => _autoMapper.Map(x, new UpdateMeetingMinutesGET())).ToList();

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
        meeting.SecretaryUserId = updateMeetingAttendeesPost.SecretaryUserId;
        meeting.ChairPersonUserId = updateMeetingAttendeesPost.ChairPersonUserId;
        meeting.Attendees = InMap(updateMeetingAttendeesPost.Attendees, meeting);
        return meeting;
    }

    public Meeting InMap(UpdateMeetingPOST updateMeetingPost, Meeting meeting)
    {
        meeting.Title = updateMeetingPost.Title;
        meeting.Description = updateMeetingPost.Description;
        meeting.Duration = updateMeetingPost.Duration;
        meeting.Frequency = updateMeetingPost.Frequency;
        meeting.Link = updateMeetingPost.Link;
        meeting.Venue = updateMeetingPost.Venue;
        meeting.IsAttendanceSaved = updateMeetingPost.IsAttendanceSaved;
        meeting.IsAttendanceTaken = updateMeetingPost.IsAttendanceTaken;
        meeting.DateTime = updateMeetingPost.DateTime;
        meeting.SecretaryUserId = updateMeetingPost.SecretaryUserId;
        meeting.ChairPersonUserId = updateMeetingPost.ChairPersonUserId;
        meeting.IsMeetingPackDownloadable = updateMeetingPost.IsMeetingPackDownloadable;
        meeting.IsMeetingPackPublished = updateMeetingPost.IsMeetingPackPublished;
        meeting.IsPast = updateMeetingPost.IsPast;
        meeting.MinutesStatus = updateMeetingPost.MinutesStatus;
        meeting.NoticesStatus = updateMeetingPost.NoticesStatus;
        meeting.Status = updateMeetingPost.Status;
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
        result.IsPresent = updateMeetingAttendeesPost.IsPresent;
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
        agendaItem.ActionRequired = agendaItemPost.ActionRequired;
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
        agendaItem.ActionRequired = agendaItemPost.ActionRequired;
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
        agendaItem.ActionRequired = agendaItemPost.ActionRequired;
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

    public Meeting InMap(List<UpdateMeetingMinutesPOST> updateMinutesPOST, Meeting existingMeeting)
    {
        var minuteList = new List<Minute>();
        foreach (var x in updateMinutesPOST)
        {
            var minute = existingMeeting.Minutes.FirstOrDefault(y => y.Id == x.Id) ?? new Minute();
            minute.MinuteText = x.MinuteText;
            minute.Attachment = x.Attachment;
            minute.CompanyId = existingMeeting.CompanyId;
            minute.MeetingId = existingMeeting.Id;
            minute.IsApproved = x.IsApproved;
            minuteList.Add(minute);
        };

        existingMeeting.Minutes = minuteList;
        return existingMeeting;
    }

    public Meeting InMap(AddMinutePOST source, Meeting existingMeeting)
    {
        existingMeeting.Minutes = InMap(source, existingMeeting, new List<Minute>());
        return existingMeeting;
    }
    public List<Minute> InMap(AddMinutePOST source,Meeting existingMeeting, List<Minute> destination = null)
    {
        var returnModel = new List<Minute>();
        
        foreach(var item in source.items)
        {
            returnModel.Add(InMap(item, existingMeeting));
        }
        return returnModel;
    }

    public Minute InMap(AddMinuteDTO source, Meeting existingMeeting)
    {
        var destination = existingMeeting.Minutes.FirstOrDefault(x => x.Id == source.Id) ?? new Minute();

        destination.MinuteText = source.MinuteText;
        destination.MeetingId = existingMeeting.Id;
        destination.CompanyId = existingMeeting.CompanyId;
        destination.AgendaItemId = source.AgendaItemId;
        return destination;
    }

    public StandAloneMinute InMap(UploadMinutePOST source, Meeting existingMeeting)
    {
        var destination = new StandAloneMinute
        {
            MeetingId = existingMeeting.Id,
            Attachment = InMap(source.Attachment,existingMeeting)
        };

        return destination;
    }

    private Attachment InMap(AttachmentPostDTO source, Meeting existingMeeting)
    {
        var destination = new Attachment();

        if (source.Identity == null) throw new BadRequestException("Document must have an attachment");

        destination.CompanyId = existingMeeting.CompanyId;
        destination.CreatedBy = existingMeeting.CreatedBy;
        destination.CategoryId = destination.Id;
        destination.Source = source.Source;
        destination.StatusDescription = source.StatusDescription;
        destination.Title = source.Title;
        destination.Highlight = source.Highlight;
        destination.HasExpiryDate = source.HasExpiryDate;
        destination.OtherDetails = source.OtherDetails;
        destination.DocumentStatus = source.DocumentStatus;
        destination.Reference = source.Reference;
        destination.ReferenceDate = source.ReferenceDate;
        destination.ReferenceDescription = source.ReferenceDescription;
        destination.ValidFrom = source.ValidFrom;
        destination.ValidTo = source.ValidTo;
        destination.FileId = source.Identity.FileId;
        destination.FileName = source.Identity.FileName;
        destination.FileSize = source.Identity.FileSize;
        destination.FileType = source.Identity.FileType;

        return destination;
    }

    public List<MinuteGET> OutMap(List<Minute> source, MinuteGET destination)
    {
        var returnModel = _autoMapper.Map<List<MinuteGET>>(source.ToList());
        return returnModel;
    }
    public List<UploadMinutePOST> OutMap(List<Minute> source, UploadMinutePOST destination)
    {
        var returnModel = _autoMapper.Map<List<UploadMinutePOST>>(source.ToList());
        return returnModel;
    }

    #endregion
}
