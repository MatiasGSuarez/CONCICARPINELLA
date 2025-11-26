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
    public class EstudioBusiness : BusinessBase<Estudio>
    {
        private readonly PacienteBusiness pacienteBusiness;
        private readonly MedicoBusiness medicoBusiness;
        private readonly PrestadorBusiness prestadorBusiness;

        public EstudioBusiness(UnitOfWork unitOfWork, PacienteBusiness pacienteBusiness, MedicoBusiness medicoBusiness, PrestadorBusiness prestadorBusiness) : base(unitOfWork)
        {
            this.pacienteBusiness = pacienteBusiness;
            this.medicoBusiness = medicoBusiness;
            this.prestadorBusiness = prestadorBusiness;
        }

        public async Task<Estudio> ProcesarSolicitudEstudioAsync(SolicitudEstudioDTO dto)
        {
            // Validar prestador
            var prestador = await prestadorBusiness.GetByIdAsync(dto.PrestadorId);
            if (prestador == null)
                throw new Exception($"El prestador con ID {dto.PrestadorId} no existe");

            // Procesar paciente
            int pacienteId = await pacienteBusiness.ProcesarPacienteAsync(dto.Paciente);

            // Procesar médico
            int medicoId = await medicoBusiness.ProcesarMedicoAsync(dto.Medico);

            // Aplicar regla de transformación de código
            string codigoEstudio = GenerarCodigoEstudio(dto.Estudio.Codigo, dto.Paciente.FechaNacimiento);

            // Crear estudio
            var nuevoEstudio = new Estudio
            {
                Codigo = codigoEstudio,
                Descripcion = dto.Estudio.Descripcion,
                FechaSolicitud = dto.Estudio.FechaSolicitud,
                PacienteId = pacienteId,
                MedicoId = medicoId,
                PrestadorId = dto.PrestadorId
            };

            int estudioId = await SaveAsync(nuevoEstudio);
            nuevoEstudio.Id = estudioId;

            return nuevoEstudio;
        }

        private string GenerarCodigoEstudio(string codigoBase, DateTime fechaNacimiento)
        {
            int edad = CalcularEdad(fechaNacimiento);
            return edad > 48 ? $"MONO-{codigoBase}" : codigoBase;
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            var hoy = DateTime.Today;
            int edad = hoy.Year - fechaNacimiento.Year;

            if (fechaNacimiento.Date > hoy.AddYears(-edad))
                edad--;

            return edad;
        }

        public async Task<IEnumerable<Estudio>> GetListByPacienteAsync(int pacienteId)
        {
            return await unitOfWork.AddRepositories.GetRepository<Estudio>()
                .GetListAsync(x => x.PacienteId == pacienteId);
        }

        public async Task<Estudio> GetByIdAsync(int id)
        {
            return await unitOfWork.AddRepositories.GetRepository<Estudio>().FindAsync(id);
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
