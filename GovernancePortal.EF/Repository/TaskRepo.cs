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

namespace GovernancePortal.EF.Repository
{
    public class TaskRepo : GenericRepo<Core.TaskManagement.TaskModel>, ITaskRepo
    {
        public PortalContext _db { get { return _context as PortalContext; } }
        public TaskRepo(PortalContext db) : base(db)
        {

        }
        public List<TaskModel> GetTaskList(string companyId, int pageNumber, int pageSize, out int totalRecords)
        {
            int skip = (pageNumber - 1) * pageSize;
            var tasks = _context.Set<TaskModel>().Where(x => x.CompanyId.Equals(companyId))
                .Include(x=>x.Items).Include(y=>y.Participants)
                       .Skip(skip)
                       .Take(pageSize)
                       .ToList();
            totalRecords = tasks.Count();
            return tasks;
        }
        public List<TaskModel> GetTaskListBySearch(string title, string companyId, int pageNumber, int pageSize, out int totalRecords)
        {
            int skip = (pageNumber - 1) * pageSize;
            var tasks = _context.Set<TaskModel>().Where(x => x.CompanyId.Equals(companyId) && x.Title.Contains(title))
                .Include(x=>x.Items).Include(y=>y.Participants)
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
            return (await _context.Set<TaskModel>().Include(x=>x.Participants).Include(x=>x.Items).ThenInclude(y=>y.Attachments)
                .FirstOrDefaultAsync(x => x.Id.Equals(taskId) && x.CompanyId.Equals(companyId)))!;
        }

        public async Task<AttendingUser> GetAttendingUsers(string meetingId, string companyId, CancellationToken token)
        {
            return (await _context.Set<AttendingUser>()
                .FirstOrDefaultAsync(x => x.Id.Equals(meetingId) && x.CompanyId.Equals(companyId), token))!;
        }

    }
}
