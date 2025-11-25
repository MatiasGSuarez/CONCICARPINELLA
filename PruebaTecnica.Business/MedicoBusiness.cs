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
    public class MedicoBusiness : BusinessBase<Medico>
    {
        public MedicoBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<Medico>> GetListAsync(string searchText)
        {
            IEnumerable<Medico> list = new List<Medico>();
            list = await unitOfWork.AddRepositories.GetRepository<Medico>()
                .GetListAsync(x => x.Nombre.ToUpper().Contains(searchText.ToUpper()));
            return list;
        }

        public async Task<Medico> GetByIdAsync(int id)
        {
            return await unitOfWork.AddRepositories.GetRepository<Medico>().FindAsync(id);
        }

        public async Task<Medico> GetByMatriculaAsync(string matricula)
        {
            var list = await unitOfWork.AddRepositories.GetRepository<Medico>().GetListAsync(x => x.Matricula == matricula);
            return list.FirstOrDefault();
        }

        // NUEVO MÉTODO: Buscar por matrícula formateada (string de 12 caracteres)
        public async Task<Medico> GetByMatriculaFormateadaAsync(string matricula)
        {
            var list = await unitOfWork.AddRepositories.GetRepository<Medico>()
                .GetListAsync(x => x.Matricula == matricula);
            return list.FirstOrDefault();
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
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await unitOfWork.AddRepositories.GetRepository<Medico>().FindAsync(id);

                if (entity == null)
                    return false;

                unitOfWork.AddRepositories.GetRepository<Medico>().Delete(entity);
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
