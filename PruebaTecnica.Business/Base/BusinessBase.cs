using PruebaTecnica.Business.Interfaces;
using PruebaTecnica.DataAccess.Generic;
using PruebaTecnica.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Business.Base
{
    public abstract class BusinessBase<TEntity> : IBusiness<TEntity> where TEntity : class
    {
        protected readonly UnitOfWork unitOfWork;

        protected BusinessBase(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public virtual async Task<TEntity> FindAsync(int id)
        {
            return await unitOfWork.AddRepositories.GetRepository<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await unitOfWork.AddRepositories.GetRepository<TEntity>().GetAsync();
        }

        public virtual async Task<PagedDataResponse<TEntity>> GetPagedResultAsync(PagingSortFilterRequest request)
        {
            Expression<Func<TEntity, bool>> filter = x => true;
            return await unitOfWork.AddRepositories.GetRepository<TEntity>().GetPagedResultAsync(
                request.FilterBy, request.FilterValue, filter, request.OrderBy, request.PageSize, request.PageIndex);
        }

        public virtual async Task<int> SaveAsync(TEntity entity)
        {
            var idProperty = typeof(TEntity).GetProperty("Id");
            int entityId = (int)idProperty.GetValue(entity);

            if (entityId == 0)
            {
                await unitOfWork.AddRepositories.GetRepository<TEntity>().AddAsync(entity);
            }
            else
            {
                unitOfWork.AddRepositories.GetRepository<TEntity>().Update(entity);
            }

            await unitOfWork.CompleteAsync();
            return entityId;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entityToDelete = await unitOfWork.AddRepositories.GetRepository<TEntity>().FindAsync(id);
            unitOfWork.Delete(entityToDelete);
            await unitOfWork.CompleteAsync();
            return true;
        }
    }
}

