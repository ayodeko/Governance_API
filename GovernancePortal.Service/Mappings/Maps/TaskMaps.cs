using AutoMapper;
using GovernancePortal.Core.General;
using GovernancePortal.Core.TaskManagement;
using GovernancePortal.Service.ClientModels.TaskManagement;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.Mappings.IMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovernancePortal.Service.Mappings.Maps
{
    public class TaskAutoMaps : Profile
    {
        public TaskAutoMaps()
        {
            CreateMap<TaskModel, TaskListGET>();
            CreateMap<TaskModel, TaskGET>();
            CreateMap<TaskModel, TaskPOST>();
            CreateMap<TaskItem, TaskItemGET>();
            CreateMap<TaskItem, TaskItemPOST>();
            CreateMap<TaskParticipant, TaskPersonGET>();
            CreateMap<TaskParticipant, TaskPersonPOST>();
        }
    }

    public class TaskMaps : ITaskMaps
    {
        private readonly IMapper _autoMapper;

        public TaskMaps()
        {
            var profiles = new List<Profile> { new TaskAutoMaps() };
            var config = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));
            _autoMapper = config.CreateMapper();
        }

        public List<TaskListGET> OutMap(List<TaskModel> source, List<TaskListGET> destination)
        {
            var returnModel = _autoMapper.Map<List<TaskListGET>>(source.ToList());
            return returnModel;
        }

        public TaskModel InMap(string companyId, TaskPOST task, TaskModel existingTask = null)
        {
            if (existingTask == null)
                existingTask = new TaskModel();

            existingTask.Title = task.Title;
            existingTask.CompanyId = companyId;
            existingTask.Description = task.Description;
            existingTask.TimeDue = task.TimeDue;
            existingTask.Items = InMap(existingTask, task.Items);
            existingTask.Participants = InMap(existingTask, task.Participants);
            return existingTask;
        }

        private List<TaskItem> InMap(TaskModel existingTask, List<TaskItemPOST> items)
        {
            var destinations = new List<TaskItem>();
            foreach (var source in items)
            {
                destinations.Add(InMap(existingTask, source));
            }
            return destinations;
        }

        private TaskItem InMap(TaskModel existingTask, TaskItemPOST item, TaskItem taskItem = null)
        {
            if (taskItem == null)
                taskItem = new TaskItem();

            taskItem.TaskId = existingTask.Id;
            taskItem.Title = item.Title;
            taskItem.IsActive = item.IsActive;
            taskItem.DocumentUpload = item.DocumentUpload;
            taskItem.Status = item.Status;
            return taskItem;
    }

        private List<TaskParticipant> InMap(TaskModel existingTask, List<TaskPersonPOST> person)
        {
            var destinations = new List<TaskParticipant>();
            foreach (var source in person)
            {
                destinations.Add(InMap(existingTask, source));
            }
            return destinations;
        }

        private TaskParticipant InMap(TaskModel existingTask, TaskPersonPOST person, TaskParticipant taskPerson = null)
        {
            if (taskPerson == null)
                taskPerson = new TaskParticipant();

            taskPerson.TaskId = existingTask.Id;
            taskPerson.UserId = person.Id;
            taskPerson.FirstName = person.FirstName;
            taskPerson.LastName = person.LastName;
            taskPerson.ImageId = person.ImageId;    
            taskPerson.Role = person.Role;
            return taskPerson;
        }
    }
}
