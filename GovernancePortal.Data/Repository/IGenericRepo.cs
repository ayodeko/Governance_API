using GovernancePortal.Core.General;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GovernancePortal.Data.Repository
{
    public interface IGenericRepo<TModel> where TModel : class, ICompanyModel
    {
        Task Add(TModel model, Person user);
        Task<IEnumerable<TModel>> FindByIds(IEnumerable<string> Ids, string companyId);
        Task<IEnumerable<TModel>> FindByLogic(Expression<Func<TModel, bool>> predicate, string companyId);
        Task<IEnumerable<TModel>> FindAll(string companyId);
        Task<IEnumerable<TModel>> FindByPage(string companyId, int pageNumber, int pageSize);
        Task<TModel> FindById(string id, string companyId);
        void Remove(TModel model);
        void RemoveRange(IEnumerable<TModel> models);
        Task<int> Count(string companyId);



    }
}
