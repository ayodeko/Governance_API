using System.Threading.Tasks;
using GovernancePortal.Core.Meetings;

namespace GovernancePortal.Data.Repository
{
    public interface IMeetingsRepo : IGenericRepo<Meeting>
    {
        Task<Meeting> FindById_Attendees_AgendaItems(string id, string companyId);
    }
}