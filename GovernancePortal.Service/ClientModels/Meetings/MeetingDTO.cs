﻿using System;
using System.Collections.Generic;
using GovernancePortal.Core.General;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Service.ClientModels.General;

namespace GovernancePortal.Service.ClientModels.Meetings
{
    public class MeetingDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        
        public MeetingFrequency Frequency { get; set; }
        public MeetingMedium Medium { get; set; }
        public MeetingType Type { get; set; }
        public int Duration { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class CreateMeetingPOST : MeetingDTO
    {
        public List<AttendingUser> Attendees { get; set; }
        public List<CreateMeetingAgendaItemDto> Items { get; set; }
    }

    public class CreateMeetingAgendaItemDto
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public bool IsNumbered { get; set; }
        public List<CreateMeetingAgendaItemDto> SubItems { get; set; }
    }
    
    public class UpdateMeetingPOST : MeetingDTO
    {
        public List<AttendingUser> Attendees { get; set; }
        public List<CreateMeetingAgendaItemDto> Items { get; set; }
    }

    public class UpdateMeetingGet : MeetingDTO
    {
        public string Id { get; set; }
    }

    public class AddPastMeetingPOST : MeetingDTO
    {
        public List<AttendingUserCreatePOST> Attendees { get; set; }
        public MinutesCreatePOST Minutes { get; set; }
    }
    
    public class AddPastMinutesPOST : MeetingDTO
    {
        public MinutesCreatePOST Minutes { get; set; }
    }
    public class AddPastAttendancePOST : MeetingDTO
    {
        public List<AttendingUserCreatePOST> Attendees { get; set; }
    }


    public class MeetingListGet : MeetingDTO
    {
        public List<AttendingUser> Attendees { get; set; }
        public string MinutesId { get; set; }
    }

    public class MeetingGET : MeetingDTO
    {
        public List<AttendingUser> Attendees { get; set; }
        public List<MeetingAgendaItem> Items { get; set; }
        public string MinutesId { get; set; }
        public string MeetingPackId { get; set; }
    }
    
    
    
    #region Minutes
    public class MinutesCreatePOST
    {
        public string MeetingId { get; set; }
        public string CompanyId { get; set; }
        public string AgendaItemId { get; set; }
        public string MinuteText { get; set; }
        public string SignerUserId { get; set; }
        public AttachmentPostDTO Attachment { get; set; }
    }
    #endregion
    
    #region AttendingUser
    public class AttendingUserCreatePOST
    {
        public string UserId { get; set; }
        public string MeetingId { get; set; }
        public string MeetingAttendanceId { get; set; }
        public bool IsPresent { get; set; }
        public bool IsGuest { get; set; }
        public string Name { get; set; }
        public AttendeePosition AttendeePosition { get; set; }
    }
    #endregion
}