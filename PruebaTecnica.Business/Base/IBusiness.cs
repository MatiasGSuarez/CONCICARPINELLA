using PruebaTecnica.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Business.Interfaces
{
    public interface IBusiness<TEntity> where TEntity : class
    {
        Task<TEntity> FindAsync(int id);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<PagedDataResponse<TEntity>> GetPagedResultAsync(PagingSortFilterRequest request);
        Task<int> SaveAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
