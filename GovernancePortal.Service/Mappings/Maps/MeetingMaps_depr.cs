using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Core.TaskManagement;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.ClientModels.TaskManagement;
using GovernancePortal.Service.Mappings.IMaps;

namespace GovernancePortal.Service.Mappings.Maps
{
    class MeetingAutoMapper : Profile
    {
        public MeetingAutoMapper()
        {
            CreateMap<CreateMeetingPOST, Meeting>();
            CreateMap<CreateMeetingAgendaItemDto, MeetingAgendaItem>();
            CreateMap<Meeting,  MeetingListGet>();
            CreateMap<Meeting,  MeetingGET>();
            CreateMap<AddPastMinutesPOST,  Meeting>();
            CreateMap<MinutesCreatePOST,  Minutes>();
            //CreateMap<MeetingModel, MeetingListGET>().ForMember(x => x.AttendanceId, option => option.MapFrom(y => y.Attendance.Id));
        }
    }
    public class MeetingMaps_depr : IMeetingMaps_depr
    {
        private IMapper _autoMapper;
        public MeetingMaps_depr()
        {
            var profiles = new List<Profile>() { new MeetingAutoMapper() };
            var mapperConfiguration = new MapperConfiguration(config => config.AddProfiles(profiles));
            _autoMapper = mapperConfiguration.CreateMapper();
        }
        public Meeting InMap(CreateMeetingPOST source,  Meeting destination) => _autoMapper.Map(source, destination);
        public Meeting InMap(UpdateMeetingPOST source,  Meeting destination) =>_autoMapper.Map(source, destination);
        public Meeting InMap(AddPastMeetingPOST source,  Meeting destination) => _autoMapper.Map(source, destination);
        public Meeting InMap(AddPastMinutesPOST source,  Meeting destination) => _autoMapper.Map(source, destination);
        public Meeting InMap(AddPastAttendancePOST source, Meeting destination) => _autoMapper.Map(source, destination);

        public List<MeetingListGet> OutMap(List<Meeting> source) => source.Select(x => _autoMapper.Map(x, new MeetingListGet())).ToList();

        public MeetingGET OutMap(Meeting source,  MeetingGET destination) =>  _autoMapper.Map(source, destination);
    }
}