using GovernancePortal.Core.Meetings;
using GovernancePortal.Data.Repository;

namespace GovernancePortal.EF.Repository
{
    public class MeetingRepo : GenericRepo<Meeting>, IMeetingsRepo
        
    {
        public PortalContext _db { get { return _context as PortalContext; } }
        public MeetingRepo(PortalContext db) : base(db)
        {

        }
    }
}