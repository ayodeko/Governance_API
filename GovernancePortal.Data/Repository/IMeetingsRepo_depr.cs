using System.Collections.Generic;
using System.Threading.Tasks;
using GovernancePortal.Core.Meetings;

namespace GovernancePortal.Data.Repository
{
    public interface IMeetingsRepo_depr : IGenericRepo<Meeting>
    {
        Task<Meeting> FindById_Attendees_AgendaItems(string id, string companyId);
        IEnumerable<Meeting> FindBySearchString(string searchString, string companyId);
    }
}