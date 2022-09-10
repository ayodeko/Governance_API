using System;
using System.Collections.Generic;
using GovernancePortal.Core.General;

namespace GovernancePortal.Core.Meetings
{
    public class MeetingModel : ICompanyModel
    {
        public MeetingModel()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ChairPersonId { get; set; }
        public string SecretaryId { get; set; }
        
        public MeetingFrequency Frequency { get; set; }
        public MeetingMedium Medium { get; set; }
        public MeetingType Type { get; set; }
        public DateTime DateTime { get; set; }
        public List<string> AttendeeIds { get; set; }
        public List<MeetingAgendaItem> Items { get; set; }
        public List<string> GuestIds { get; set; }
    }

    public enum MeetingType
    {
        Board,
        Management,
        Others
    }

    public enum MeetingFrequency
    {
        Daily, Weekly, Monthly
    }

    public enum MeetingMedium
    {
        Physical, Virtual, Hybrid
    }
    
}