using GovernancePortal.Data.Repository;
using GovernancePortal.Core.General;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GovernancePortal.EF.Repository
{
    public class GenericRepo<TModel>: IGenericRepo<TModel> where TModel : class, ICompanyModel
    {
        protected readonly DbContext _context;
        public GenericRepo(DbContext context)
        {
            _context = context;
        }

        public async Task Add(TModel model, UserModel user)
        {
            model.DateCreated = DateTime.Now;
            model.CompanyId = user.CompanyId;
            model.CreatedBy = user.Id;
            await _context.Set<TModel>().AddAsync(model);
        }

        public async Task<int> Count(string companyId)
        {
            return await _context.Set<TModel>().Where(x => x.CompanyId.Equals(companyId)).CountAsync();
        }

        public Task<IEnumerable<TModel>> FindAll(string companyId)
        {
            throw new NotImplementedException();
        }

        public async Task<TModel> FindById(string id, string companyId)
        {
            return await _context.Set<TModel>()
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.CompanyId.Equals(companyId));
        }

        public Task<IEnumerable<TModel>> FindByIds(IEnumerable<string> Ids, string companyId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TModel>> FindByLogic(Expression<Func<TModel, bool>> predicate, string companyId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TModel>> FindByPage(string companyId, int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var result = _context.Set<TModel>().Where(x => x.CompanyId.Equals(companyId))
                .Skip(skip)
                .Take(pageSize)
                .AsEnumerable();
            return result;
        }

        public void Remove(TModel model)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<TModel> models)
        {
            throw new NotImplementedException();
        }
    }
}
