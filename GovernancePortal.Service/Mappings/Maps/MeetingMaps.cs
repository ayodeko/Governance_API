using System.Collections.Generic;
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
            CreateMap<MeetingModel, CreateMeetingPOST>();
        }
    }
    public class MeetingMaps : IMeetingMaps
    {
        private IMapper _autoMapper;
        public MeetingMaps()
        {
            var profiles = new List<Profile>() { new MeetingAutoMapper() };
            var mapperConfiguration = new MapperConfiguration(config => config.AddProfiles(profiles));
            _autoMapper = mapperConfiguration.CreateMapper();
        }
        public MeetingModel InMap(CreateMeetingPOST source,  MeetingModel destination)
        {
            return _autoMapper.Map(source, destination);
        }
        
        
        public MeetingModel InMap(UpdateMeetingPOST source,  MeetingModel destination)
        {
            return _autoMapper.Map(source, destination);
        }
    }
}