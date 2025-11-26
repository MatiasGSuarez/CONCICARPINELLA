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
    public class MedicoBusiness : BusinessBase<Medico>
    {
        public MedicoBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Medico> CreateMedicoAsync(MedicoDTO dto)
        {
            string matriculaFormateada = FormatearMatricula(dto.Matricula);

            var medicoExistente = await GetByMatriculaFormateadaAsync(matriculaFormateada);
            if (medicoExistente != null)
                throw new Exception($"Ya existe un médico con la matrícula {dto.Matricula}");

            var nuevoMedico = new Medico
            {
                Nombre = dto.Nombre,
                Matricula = matriculaFormateada
            };

            int medicoId = await SaveAsync(nuevoMedico);
            nuevoMedico.Id = medicoId;

            return nuevoMedico;
        }

        public async Task<Medico> UpdateMedicobyMatriculaAsync(string matricula, MedicoDTO dto)
        {
            string matriculaFormateada = FormatearMatricula(matricula);
            var medico = await GetByMatriculaFormateadaAsync(matriculaFormateada);
            if (medico == null)
                throw new Exception($"No se encontró un médico con matricula: {matriculaFormateada}");

            medico.Nombre = dto.Nombre;
            medico.Matricula = FormatearMatricula(dto.Matricula);

            await SaveAsync(medico);
            return medico;
        }

        public async Task<int> ProcesarMedicoAsync(MedicoSolicitudDTO dto)
        {
            string matriculaFormateada = FormatearMatricula(dto.Matricula);
            var medicoExistente = await GetByMatriculaFormateadaAsync(matriculaFormateada);

            if (medicoExistente == null)
            {
                var nuevoMedico = new Medico
                {
                    Nombre = dto.Nombre,
                    Matricula = matriculaFormateada
                };
                return await SaveAsync(nuevoMedico);
            }

            medicoExistente.Nombre = dto.Nombre;
            return await SaveAsync(medicoExistente);
        }

        public async Task<Medico> GetByIdAsync(int id)
        {
            return await unitOfWork.AddRepositories.GetRepository<Medico>().FindAsync(id);
        }

        public async Task<Medico> GetByMatriculaFormateadaAsync(string matricula)
        {
            var list = await unitOfWork.AddRepositories.GetRepository<Medico>().GetListAsync(x => x.Matricula == matricula);
            return list.FirstOrDefault();
        }

        private string FormatearMatricula(string matricula)
        {
            if (string.IsNullOrWhiteSpace(matricula))
                throw new Exception("La matrícula no puede estar vacía");
            
            if (matricula.Length > 12)
                throw new Exception("La matrícula no debe tener mas de 12 caracteres");

            return matricula.PadLeft(12, '0');
        }

        public async Task<IEnumerable<Medico>> GetListAsync(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return await GetAsync();

            return await unitOfWork.AddRepositories.GetRepository<Medico>().GetListAsync(x => x.Nombre.ToUpper().Contains(searchText.ToUpper()) || x.Matricula.Contains(searchText));
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
                throw new Exception("Error al guardar el médico", ex);
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
                throw new Exception("Error al eliminar el médico", ex);
            }
        }
    }
}
