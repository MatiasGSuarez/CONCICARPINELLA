using PruebaTecnica.Business.Base;
using PruebaTecnica.DataAccess.Generic;
using PruebaTecnica.Model.DTO;
using PruebaTecnica.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Business
{
    public class PacienteBusiness : BusinessBase<Paciente>
    {
        public PacienteBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<int> ProcesarPacienteAsync(PacienteSolicitudDTO dto)
        {
            var pacienteExistente = await GetByDniAsync(dto.Dni);

            if (pacienteExistente != null)
                return pacienteExistente.Id;

            var nuevoPaciente = new Paciente
            {
                Dni = dto.Dni,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                FechaNacimiento = dto.FechaNacimiento
            };

            return await SaveAsync(nuevoPaciente);
        }

        public async Task<IEnumerable<Paciente>> GetListAsync(string searchText)
        {
            IEnumerable<Paciente> list = new List<Paciente>();
            list = await unitOfWork.AddRepositories.GetRepository<Paciente>()
                .GetListAsync(x => x.Nombre.ToUpper().Contains(searchText.ToUpper()) || x.Apellido.ToUpper().Contains(searchText.ToUpper()) || x.Dni.Contains(searchText));
            return list;
        }

        public async Task<Paciente> GetByIdAsync(int id)
        {
            return await unitOfWork.AddRepositories.GetRepository<Paciente>().FindAsync(id);
        }

        public async Task<Paciente> GetByDniAsync(string dni)
        {
            var list = await unitOfWork.AddRepositories.GetRepository<Paciente>().GetListAsync(x => x.Dni == dni);
            return list.FirstOrDefault();
        }

        public virtual async Task<int> SaveAsync(Paciente entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    await unitOfWork.AddRepositories.GetRepository<Paciente>().AddAsync(entity);
                }
                else
                {
                    unitOfWork.AddRepositories.GetRepository<Paciente>().Update(entity);
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
                var entity = await unitOfWork.AddRepositories.GetRepository<Paciente>().FindAsync(id);

                if (entity == null)
                    return false;

                unitOfWork.AddRepositories.GetRepository<Paciente>().Delete(entity);
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
