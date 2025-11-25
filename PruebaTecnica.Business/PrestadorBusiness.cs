using PruebaTecnica.Business.Base;
using PruebaTecnica.DataAccess.Generic;
using PruebaTecnica.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Business
{
    public class PrestadorBusiness : BusinessBase<Prestador>
    {
        public PrestadorBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<Prestador>> GetListAsync(string searchText)
        {
            IEnumerable<Prestador> list = new List<Prestador>();
            list = await unitOfWork.AddRepositories.GetRepository<Prestador>()
                .GetListAsync(x => x.Nombre.ToUpper().Contains(searchText.ToUpper()));
            return list;
        }

        public async Task<Prestador> GetByIdAsync(int id)
        {
            return await unitOfWork.AddRepositories.GetRepository<Prestador>().FindAsync(id);
        }

        public virtual async Task<int> SaveAsync(Prestador entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    await unitOfWork.AddRepositories.GetRepository<Prestador>().AddAsync(entity);
                }
                else
                {
                    // Verificar si la entidad existe antes de actualizar
                    var existingEntity = await unitOfWork.AddRepositories.GetRepository<Prestador>()
                        .FindAsync(entity.Id);

                    if (existingEntity == null)
                        throw new Exception($"El prestador con ID {entity.Id} no existe");

                    // Actualizar las propiedades
                    existingEntity.Nombre = entity.Nombre;

                    unitOfWork.AddRepositories.GetRepository<Prestador>().Update(existingEntity);
                }
                await unitOfWork.CompleteAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await unitOfWork.AddRepositories.GetRepository<Prestador>().FindAsync(id);

                if (entity == null)
                    return false;

                unitOfWork.AddRepositories.GetRepository<Prestador>().Delete(entity);
                await unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
