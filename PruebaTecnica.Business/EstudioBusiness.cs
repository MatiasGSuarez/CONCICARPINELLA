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
    public class EstudioBusiness : BusinessBase<Estudio>
    {
        public EstudioBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<Estudio>> GetListAsync(string searchText)
        {
            IEnumerable<Estudio> list = new List<Estudio>();
            list = await unitOfWork.AddRepositories.GetRepository<Estudio>()
                .GetListAsync(x => x.Codigo.ToUpper().Contains(searchText.ToUpper()) || x.Descripcion.ToUpper().Contains(searchText.ToUpper()));
            return list;
        }

        public async Task<IEnumerable<Estudio>> GetListByPacienteAsync(int pacienteId)
        {
            return await unitOfWork.AddRepositories.GetRepository<Estudio>()
                .GetListAsync(x => x.PacienteId == pacienteId);
        }

        public async Task<IEnumerable<Estudio>> GetListByMedicoAsync(int medicoId)
        {
            return await unitOfWork.AddRepositories.GetRepository<Estudio>()
                .GetListAsync(x => x.MedicoId == medicoId);
        }

        public async Task<IEnumerable<Estudio>> GetListByPrestadorAsync(int prestadorId)
        {
            return await unitOfWork.AddRepositories.GetRepository<Estudio>()
                .GetListAsync(x => x.PrestadorId == prestadorId);
        }

        public async Task<IEnumerable<Estudio>> GetListByFechaAsync(DateTime fechaDesde, DateTime fechaHasta)
        {
            return await unitOfWork.AddRepositories.GetRepository<Estudio>()
                .GetListAsync(x => x.FechaSolicitud >= fechaDesde &&
                                  x.FechaSolicitud <= fechaHasta);
        }

        public async Task<Estudio> GetByIdAsync(int id)
        {
            return await unitOfWork.AddRepositories.GetRepository<Estudio>().FindAsync(id);
        }

        public async Task<Estudio> GetByCodigoAsync(string codigo)
        {
            var list = await unitOfWork.AddRepositories.GetRepository<Estudio>()
                .GetListAsync(x => x.Codigo == codigo);
            return list.FirstOrDefault();
        }

        public virtual async Task<int> SaveAsync(Estudio entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    await unitOfWork.AddRepositories.GetRepository<Estudio>().AddAsync(entity);
                }
                else
                {
                    unitOfWork.AddRepositories.GetRepository<Estudio>().Update(entity);
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
                var entity = await unitOfWork.AddRepositories.GetRepository<Estudio>().FindAsync(id);

                if (entity == null)
                    return false;

                unitOfWork.AddRepositories.GetRepository<Estudio>().Delete(entity);
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
