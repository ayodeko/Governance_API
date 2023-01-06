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
using Microsoft.AspNetCore.Authentication;
using GovernancePortal.Service.ClientModels.Exceptions;

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
            CreateMap<TaskAttachment, AttachmentIdentityDTO>();
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
            if (source is null) return new List<TaskListGET>();
            var returnModel = _autoMapper.Map<List<TaskListGET>>(source.ToList());
            return returnModel;
        }

        public TaskModel InMap(UserModel user, TaskPOST task, TaskModel existingTask = null)
        {
            bool isUpdate = false;
            if (existingTask == null)
            {
                existingTask = new TaskModel();
            }

            existingTask.Title = task.Title;
            existingTask.CompanyId = user.CompanyId;
            existingTask.Description = task.Description;
            existingTask.CreatedBy = user.Id;
            existingTask.CreatorImageId = user.ImageId;
            existingTask.CreatorName = user.FirstName + user.LastName;
            existingTask.TimeDue = task.TimeDue;
            if (task.isMeetingRelated)
            {
                existingTask.MeetingId = task.meetingId;
                existingTask.IsMeetingRelated = true;
            }
            existingTask.Items.AddRange(InMap(existingTask, task.Items));
                existingTask.Participants = InMap(existingTask, task.Participants);
            return existingTask;
        }

        private List<TaskItem> InMap(TaskModel existingTask, List<TaskItemPOST> items)
        {
            var destinations = new List<TaskItem>();
            foreach (var source in items)
            {
                var taskItem = existingTask.Items.Where(x => x.Id == source.Id).FirstOrDefault();
                if(taskItem is null)
                {
                    destinations.Add(InMap(existingTask, source));

                }
            }
            return destinations;
        }

        public TaskItem InMap(TaskModel existingTask, TaskItemPOST item, TaskItem taskItem = null)
        {
            if (taskItem == null)
            {
                taskItem = new TaskItem();
                taskItem.DateCreated = DateTime.Now;
            }

            taskItem.TaskId = existingTask.Id;
            taskItem.Title = item.Title;
            taskItem.IsActive = item.IsActive;
            taskItem.DocumentUpload = item.DocumentUpload;
            taskItem.Status = item.Status;
            taskItem.Attachments = InMap(taskItem.Id, item.Attachments);
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
        public TaskGET OutMap(TaskModel source, TaskGET destination )
        {
            var returnModel = _autoMapper.Map<TaskGET>(source);
            returnModel.Items = OutMap(source.Items, new List<TaskItemGET>()); 
            returnModel.Participants = _autoMapper.Map<List<TaskPersonGET>>(source.Participants);
            return returnModel;
        }

        public List<TaskItemGET> OutMap(List<TaskItem> source, List<TaskItemGET> destination)
        {
            foreach (var item in source)
            {
                var toAdd = _autoMapper.Map<TaskItemGET>(item);
                var doc = item.Attachments;
                toAdd.Attachments = OutMap(doc, new List<AttachmentIdentityDTO>());
                destination.Add(toAdd);
            }
            return destination;
        }
        private List<AttachmentIdentityDTO> OutMap(List<TaskAttachment> source, List<AttachmentIdentityDTO> destination)
        {
            foreach (var item in source)
            {
                destination.Add(OutMap(item, new AttachmentIdentityDTO())); 
            }
            return destination;
        }
        private AttachmentIdentityDTO OutMap(TaskAttachment source, AttachmentIdentityDTO destinations)
        {
            if (source != null) {
                destinations.FileId = source.FileId;
                destinations.FileName = source.FileName;
                destinations.FileSize = source.FileSize;
            }
            return destinations;
        }

        public TaskItem InMap(AddDocumentToTaskItemDTO source, TaskItem existingTaskitem)
        {

            existingTaskitem.Attachments = InMap(existingTaskitem.Id, source.Documents);
            return existingTaskitem;
        }
        public List<TaskAttachment> InMap(string catgeoryId, List<AttachmentPostDTO> attachments)
        {
            if (attachments == null) return new List<TaskAttachment>();
            var newAttachments = new List<TaskAttachment>();
            var invalidAttachments = new List<AttachmentPostDTO>();
            foreach (var attachment in attachments)
            {
                if (attachment.Identity == null)
                    continue;

                newAttachments.Add(InMap(catgeoryId, attachment));
            }

            //remove all non persistent values
            //foreach (var item in invalidAttachments)
            //{
            //    attachments.Remove(item);
            //}

            return newAttachments;
        }
        private TaskAttachment InMap( string categoryId, AttachmentPostDTO attachment1, TaskAttachment attachment2 = null)
        {
            if (attachment2 == null)
                attachment2 = new TaskAttachment();

            if (attachment1.Identity == null) throw new BadRequestException("Document must have an attachment");

            //attachment2.CompanyId = user.CompanyId;
            //attachment2.CreatedBy = user.Id;
            attachment2.CategoryId = categoryId;
            attachment2.Title = attachment1.Title;
            attachment2.FileId = attachment1.Identity.FileId;
            attachment2.FileName = attachment1.Identity.FileName;
            attachment2.FileSize = attachment1.Identity.FileSize;
            attachment2.FileType = attachment1.Identity.FileType;

            return attachment2;
        }
    }
}
