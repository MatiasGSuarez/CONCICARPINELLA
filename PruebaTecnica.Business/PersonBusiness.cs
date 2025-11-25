using PruebaTecnica.Business.Base;
using PruebaTecnica.DataAccess.Generic;
using PruebaTecnica.Model.Model;

namespace PruebaTecnica.Business
{
    public class PersonBusiness : BusinessBase<Medico>
    {
        public PersonBusiness(UnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public async Task<IEnumerable<Medico>> GetListAsync(string sellerName)
        {
            IEnumerable<Medico> list = new List<Medico>();
            list = await unitOfWork.AddRepositories.GetRepository<Medico>().GetListAsync(x => x.Nombre.ToUpper().Contains(sellerName.ToUpper()));
            return list;
        }

        public virtual async Task<int> SaveAsync(Medico entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    await unitOfWork.AddRepositories.GetRepository<Medico>().AddAsync(entity);
                }
                else
                {
                    unitOfWork.AddRepositories.GetRepository<Medico>().Update(entity);
                }
                await unitOfWork.CompleteAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
    }
}
