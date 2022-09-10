using System;
using System.Collections.Generic;
using GovernancePortal.Core.Meetings;

namespace GovernancePortal.Service.ClientModels.Meetings
{
    public class MeetingDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ChairPersonId { get; set; }
        public string SecretaryId { get; set; }
        
        public MeetingFrequency Frequency { get; set; }
        public MeetingMedium Medium { get; set; }
        public MeetingType Type { get; set; }
        public DateTime DateTime { get; set; }
        public List<string> AttendeesId { get; set; }
        public List<MeetingAgendaItem> Items { get; set; }
    }

    public class CreateMeetingPOST : MeetingDTO
    {
        
    }
    
    public class UpdateMeetingPOST : MeetingDTO
    {
        
    }

    public class UpdateMeetingGet : MeetingDTO
    {
        public string Id { get; set; }
    }
    
}