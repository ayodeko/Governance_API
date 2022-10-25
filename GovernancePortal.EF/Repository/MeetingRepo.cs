using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace GovernancePortal.EF.Repository
{
    public class MeetingRepo : GenericRepo<Meeting>, IMeetingsRepo
        
    {
        public PortalContext _db { get { return _context as PortalContext; } }
        public MeetingRepo(PortalContext db) : base(db)
        {
            
        }

        public async Task<Meeting> FindById_Attendees_AgendaItems(string id, string companyId)
        {
            return await _context.Set<Meeting>()
                .Include(x => x.Attendees)
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.CompanyId.Equals(companyId));
        }
        
        public async Task<IEnumerable<Meeting>> FindByPage_Attendees(string companyId, int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var result = _context.Set<Meeting>().Where(x => x.CompanyId.Equals(companyId))
                .Include(x => x.Attendees)
                .Skip(skip)
                .Take(pageSize)
                .AsEnumerable();
            return result;
        }
    }
}