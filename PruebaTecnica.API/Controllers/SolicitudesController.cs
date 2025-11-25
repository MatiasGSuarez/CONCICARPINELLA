using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Business;
using PruebaTecnica.Model.BaseDTO;
using PruebaTecnica.Model.DTO;
using PruebaTecnica.Model.Model;

namespace PruebaTecnica.API.Controllers
{
    namespace PruebaTecnica.API.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class SolicitudesController : Controller
        {
            private readonly IHttpContextAccessor contextAccessor;
            private readonly IMapper mapper;
            private readonly PacienteBusiness pacienteBusiness;
            private readonly MedicoBusiness medicoBusiness;
            private readonly EstudioBusiness estudioBusiness;
            private readonly PrestadorBusiness prestadorBusiness;

            public SolicitudesController( IMapper mapper, IHttpContextAccessor contextAccessor, PacienteBusiness pacienteBusiness,
                MedicoBusiness medicoBusiness, EstudioBusiness estudioBusiness, PrestadorBusiness prestadorBusiness)
            {
                this.mapper = mapper;
                this.contextAccessor = contextAccessor;
                this.pacienteBusiness = pacienteBusiness;
                this.medicoBusiness = medicoBusiness;
                this.estudioBusiness = estudioBusiness;
                this.prestadorBusiness = prestadorBusiness;
            }

            [HttpPost()]
            public async Task<ActionResult<SolicitudResponseDTO>> ProcesarSolicitud([FromBody] SolicitudEstudioDTO dto)
            {
                try
                {
                    if (dto == null)
                        return BadRequest(new ActionResultDTO { Message = "Datos inválidos" });

                    // Validar que el prestador exista
                    var prestador = await prestadorBusiness.GetByIdAsync(dto.PrestadorId);
                    if (prestador == null)
                        return BadRequest(new ActionResultDTO { Message = $"El prestador con ID {dto.PrestadorId} no existe" });

                    // 1. Procesar Paciente - Insertar si no existe
                    var pacienteExistente = await pacienteBusiness.GetByDniAsync(dto.Paciente.Dni);
                    int pacienteId;

                    if (pacienteExistente == null)
                    {
                        var nuevoPaciente = new Paciente
                        {
                            Dni = dto.Paciente.Dni,
                            Nombre = dto.Paciente.Nombre,
                            Apellido = dto.Paciente.Apellido,
                            FechaNacimiento = dto.Paciente.FechaNacimiento
                        };
                        pacienteId = await pacienteBusiness.SaveAsync(nuevoPaciente);
                    }
                    else
                    {
                        pacienteId = pacienteExistente.Id;
                    }

                    // 2. Procesar Médico - Insertar o actualizar según matrícula
                    // Validar y formatear matrícula a 12 caracteres
                    string matriculaFormateada = dto.Medico.Matricula.PadLeft(12, '0');

                    var medicoExistente = await medicoBusiness.GetByMatriculaFormateadaAsync(matriculaFormateada);
                    int medicoId;

                    if (medicoExistente == null)
                    {
                        var nuevoMedico = new Medico
                        {
                            Nombre = dto.Medico.Nombre,
                            Matricula = matriculaFormateada
                        };
                        medicoId = await medicoBusiness.SaveAsync(nuevoMedico);
                    }
                    else
                    {
                        medicoExistente.Nombre = dto.Medico.Nombre;
                        medicoId = await medicoBusiness.SaveAsync(medicoExistente);
                    }

                    // 3. Calcular edad del paciente y aplicar regla de transformación
                    int edad = CalcularEdad(dto.Paciente.FechaNacimiento);
                    string codigoEstudio = dto.Estudio.Codigo;

                    if (edad > 48)
                    {
                        codigoEstudio = $"MONO-{codigoEstudio}";
                    }

                    // 4. Crear el estudio
                    var nuevoEstudio = new Estudio
                    {
                        Codigo = codigoEstudio,
                        Descripcion = dto.Estudio.Descripcion,
                        FechaSolicitud = dto.Estudio.FechaSolicitud,
                        PacienteId = pacienteId,
                        MedicoId = medicoId,
                        PrestadorId = dto.PrestadorId
                    };

                    int estudioId = await estudioBusiness.SaveAsync(nuevoEstudio);

                    // 5. Retornar respuesta
                    var response = new SolicitudResponseDTO
                    {
                        EstudioId = estudioId,
                        CodigoEstudioGenerado = codigoEstudio,
                        Message = "La solicitud de estudio se procesó correctamente"
                    };

                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new ActionResultDTO { Message = $"Error interno: {ex.Message}" });
                }
            }

            [HttpGet()]
            public async Task<ActionResult<IList<EstudioDTO>>> GetAllEstudios()
            {
                try
                {
                    var list = await estudioBusiness.GetAsync();
                    if (list == null || !list.Any())
                        return NotFound(new ActionResultDTO { Message = "No se encontraron estudios" });

                    var dto = mapper.Map<IList<EstudioDTO>>(list);
                    return Ok(dto);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new ActionResultDTO { Message = $"Error interno: {ex.Message}" });
                }
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<EstudioDTO>> GetEstudioById(int id)
            {
                try
                {
                    var item = await estudioBusiness.GetByIdAsync(id);
                    if (item == null)
                        return NotFound(new ActionResultDTO { Message = $"No se encontró un estudio con el ID {id}" });

                    var dto = mapper.Map<EstudioDTO>(item);
                    return Ok(dto);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new ActionResultDTO { Message = $"Error interno: {ex.Message}" });
                }
            }

            [HttpGet("paciente/{pacienteId}")]
            public async Task<ActionResult<IList<EstudioDTO>>> GetEstudiosByPaciente(int pacienteId)
            {
                try
                {
                    var list = await estudioBusiness.GetListByPacienteAsync(pacienteId);
                    if (list == null || !list.Any())
                        return NotFound(new ActionResultDTO { Message = $"No se encontraron estudios para el paciente {pacienteId}" });

                    var dto = mapper.Map<IList<EstudioDTO>>(list);
                    return Ok(dto);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new ActionResultDTO { Message = $"Error interno: {ex.Message}" });
                }
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult<ActionResultDTO>> Delete([FromRoute] int id)
            {
                try
                {
                    var estudio = await estudioBusiness.GetByIdAsync(id);
                    if (estudio == null)
                    {
                        return NotFound(new ActionResultDTO { Message = $"No se encontró un estudio con el ID {id}" });
                    }

                    var result = await estudioBusiness.DeleteAsync(id);
                    if (result)
                        return Ok(new ActionResultDTO { Code = result.ToString(), Message = "El estudio se eliminó correctamente" });
                    else
                        return BadRequest(new ActionResultDTO { Message = "No se pudo eliminar el estudio" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new ActionResultDTO { Message = $"Error interno: {ex.Message}" });
                }
            }

            private int CalcularEdad(DateTime fechaNacimiento)
            {
                var hoy = DateTime.Today;
                int edad = hoy.Year - fechaNacimiento.Year;

                if (fechaNacimiento.Date > hoy.AddYears(-edad))
                    edad--;

                return edad;
            }
        }
    }
}
