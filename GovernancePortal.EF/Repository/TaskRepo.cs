using GovernancePortal.Core.TaskManagement;
using GovernancePortal.Data.Repository;
using GovernancePortal.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = GovernancePortal.Core.General.TaskStatus;
using EF = Microsoft.EntityFrameworkCore.EF;
using GovernancePortal.Core.Meetings;
using System.Threading;
using GovernancePortal.Core.General;

namespace GovernancePortal.EF.Repository
{
    public class TaskRepo : GenericRepo<Core.TaskManagement.TaskModel>, ITaskRepo
    {
        public PortalContext _db { get { return _context as PortalContext; } }
        public TaskRepo(PortalContext db) : base(db)
        {

        }
        public List<TaskModel> GetTaskList(string companyId, TaskStatus? status, string userId, string searchString, int pageNumber, int pageSize, out int totalRecords)
        {
            var skip = (pageNumber - 1) * pageSize;
            var result = new List<TaskModel>();

            if(status is TaskStatus.Due)
            {
                result = (_context.Set<TaskModel>()
                 .Include(x => x.Items).Include(y => y.Participants)
               .Where(y => Microsoft.EntityFrameworkCore.EF.Functions.DateDiffMinute(DateTime.Now, y.TimeDue) <= 0)
               .Where(x => string.IsNullOrEmpty(searchString) || x.Title.Contains(searchString))
               .Where(x => string.IsNullOrEmpty(userId) || x.Participants.Any(c => c.UserId == userId))
               .Where(x => x.CompanyId.Equals(companyId)))

               .OrderByDescending(X => X.DateCreated).Skip(skip)
                      .Take(pageSize)
                      .ToList();
            }
            else if (status is TaskStatus.Ongoing)
            {
                result = (_context.Set<TaskModel>()
                        .Include(x => x.Items).Include(y => y.Participants)
                        .Where(x => status == null || (x.Items.Any(i => i.Status == (TaskItemStatus)TaskStatus.Completed) && x.Items.Any(i => i.Status != (TaskItemStatus)TaskStatus.Completed)))
                        .Where(x => string.IsNullOrEmpty(searchString) || x.Title.Contains(searchString))
                        .Where(x => string.IsNullOrEmpty(userId) || x.Participants.All(c => c.UserId == userId))
                        .Where(x => x.CompanyId.Equals(companyId)))

                    .OrderByDescending(X => X.DateCreated).Skip(skip)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                result = (_context.Set<TaskModel>()
                 .Include(x => x.Items).Include(y => y.Participants)
               .Where(x => status == null || x.Items.All(y => y.Status == (TaskItemStatus)status))
               .Where(x => string.IsNullOrEmpty(searchString) || x.Title.Contains(searchString))
               .Where(x => string.IsNullOrEmpty(userId) || x.Participants.All(c => c.UserId == userId))
               .Where(x => x.CompanyId.Equals(companyId)))

               .OrderByDescending(X => X.DateCreated).Skip(skip)
                      .Take(pageSize)
                      .ToList();
            }

           

            totalRecords = result.Count();
            return result;

        }
        public List<TaskModel> GetTaskListBySearch(string companyId, TaskStatus? status, string userId, string searchString, int pageNumber, int pageSize, out int totalRecords)
        {
            var skip = (pageNumber - 1) * pageSize;
            var result = new List<TaskModel>();

            if (status != null && status == TaskStatus.Due)
            {
                result = (_context.Set<TaskModel>()
                 .Include(x => x.Items).Include(y => y.Participants)
               .Where(y => Microsoft.EntityFrameworkCore.EF.Functions.DateDiffMinute(DateTime.Now, y.TimeDue) <= 0)
               .Where(x => string.IsNullOrEmpty(searchString) || x.Title.Contains(searchString))
               .Where(x => string.IsNullOrEmpty(userId) || x.Participants.Any(c => c.UserId == userId))
               .Where(x => x.CompanyId.Equals(companyId)))
               .OrderByDescending(X => X.DateCreated).Skip(skip)
                      .Take(pageSize)
                      .ToList();
            }
            else
            {
                result = (_context.Set<TaskModel>()
                 .Include(x => x.Items).Include(y => y.Participants)
               .Where(x => status == null || x.Status == status)
               .Where(x => string.IsNullOrEmpty(searchString) || x.Title.Contains(searchString))
               .Where(x => string.IsNullOrEmpty(userId) || x.Participants.Any(c => c.UserId == userId))
               .Where(x => x.CompanyId.Equals(companyId)))
               .OrderByDescending(X => X.DateCreated).Skip(skip)
                      .Take(pageSize)
                      .ToList();
            }

            totalRecords = result.Count();
            return result;
        }

        public List<TaskModel> GetTaskListByStatus(TaskStatus status, string companyId, int pageNumber, int pageSize, out int totalRecords)
        {
            int skip = (pageNumber - 1) * pageSize;
            var tasks = _context.Set<TaskModel>().Where(x => x.CompanyId.Equals(companyId)).Where(y => y.Status == status)
                .Include(x => x.Items).Include(y => y.Participants)
                       .Skip(skip)
                       .Take(pageSize)
                       .ToList();
            totalRecords = tasks.Count();
            return tasks;
        }

        public List<TaskModel> GetNotStartedTasks(string companyId, int pageNumber, int pageSize, out int totalRecords)
        {
            int skip = (pageNumber - 1) * pageSize;
            var tasks = _context.Set<TaskModel>().Where(x => x.CompanyId.Equals(companyId)).Where(y => y.Status == TaskStatus.NotStarted)
                .Include(x => x.Items).Include(y => y.Participants)
                       .Skip(skip)
                       .Take(pageSize)
                       .ToList();
            totalRecords = tasks.Count();
            return tasks;
        }
        public List<TaskModel> GetOngoingTasks(string companyId, int pageNumber, int pageSize, out int totalRecords)
        {
            int skip = (pageNumber - 1) * pageSize;
            var tasks = _context.Set<TaskModel>().Where(x => x.CompanyId.Equals(companyId)).Where(y => y.Status == TaskStatus.Ongoing)
                .Include(x => x.Items).Include(y => y.Participants)
                       .Skip(skip)
                       .Take(pageSize)
                       .ToList();
            totalRecords = tasks.Count();
            return tasks;
        }
        public List<TaskModel> GetCompletedTasks(string companyId, int pageNumber, int pageSize, out int totalRecords)
        {
            int skip = (pageNumber - 1) * pageSize;
            var tasks = _context.Set<TaskModel>().Where(x => x.CompanyId.Equals(companyId)).Where(y => y.Status == TaskStatus.Completed)
                .Include(x => x.Items).Include(y => y.Participants)
                       .Skip(skip)
                       .Take(pageSize)
                       .ToList();
            totalRecords = tasks.Count();
            return tasks;
        }
        public List<TaskModel> GetDueTasks(string companyId, int pageNumber, int pageSize, out int totalRecords)
        {
            int skip = (pageNumber - 1) * pageSize;
            var tasks = _context.Set<TaskModel>().Where(x => x.CompanyId.Equals(companyId)).Where(y => Microsoft.EntityFrameworkCore.EF.Functions.DateDiffMinute(DateTime.Now, y.TimeDue) <= 0)
                .Include(x => x.Items).Include(y => y.Participants)
                       .Skip(skip)
                       .Take(pageSize)
                       .ToList();
            totalRecords = tasks.Count();
            return tasks;
        }
        public List<TaskModel> GetUserTaskListByStatus(TaskStatus status, string userId, string companyId, int pageNumber, int pageSize, out int totalRecords)
        {
            int skip = (pageNumber - 1) * pageSize;
            var result = _context.Set<TaskModel>().Include(x => x.Items).Include(y => y.Participants)
                     .Where(x => x.CompanyId.Equals(companyId) && x.Participants.Any(c => c.UserId == userId) &&  x.Status == status);
            totalRecords = result.Count();
            return result.Skip(skip)
                .Take(pageSize).ToList()!;
        }

        public List<TaskModel> GetTaskListByUserId(string userId, string companyId, int pageNumber, int pageSize,
       out int totalRecords)
        {
            var skip = (pageNumber - 1) * pageSize;
            var result = (_context.Set<TaskModel>()
                .Include(x => x.Participants)
                .Include(x => x.Items)
                .Where(x => x.CompanyId.Equals(companyId) && x.Participants.Any(c => c.UserId == userId)));
            totalRecords = result.Count();
            return result.Skip(skip)
                .Take(pageSize).ToList()!;
        }
        public async Task<TaskModel> GetTaskData(string taskId, string companyId)
        {
            return (await _context.Set<TaskModel>().Include(x=>x.Participants).Include(x=>x.Items).ThenInclude(y=>y.Attachments).OrderByDescending(X => X.DateCreated)
                .FirstOrDefaultAsync(x => x.Id.Equals(taskId) && x.CompanyId.Equals(companyId)))!;
        }
        public async Task<TaskItem> GetTaskItemData(string taskItemId, string taskId)
        {
            return (await _context.Set<TaskItem>().Include(x => x.Attachments)
                .FirstOrDefaultAsync(x => x.Id.Equals(taskItemId) && x.TaskId.Equals(taskId)))!;
        }

        public async Task<AttendingUser> GetAttendingUsers(string meetingId, string companyId, CancellationToken token)
        {
            return (await _context.Set<AttendingUser>()
                .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId), token))!;
        }

    }
}
