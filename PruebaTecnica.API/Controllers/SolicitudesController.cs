using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.API.Common;
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
                    var estudio = await estudioBusiness.ProcesarSolicitudEstudioAsync(dto);

                    var response = new SolicitudResponseDTO
                    {
                        EstudioId = estudio.Id,
                        CodigoEstudioGenerado = estudio.Codigo,
                        Message = "La solicitud de estudio se procesó correctamente"
                    };

                    return Ok(response);
                }
                catch (BusinessException bex)
                {
                    return BadRequest(new ActionResultDTO { Message = bex.Message });
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

        }
    }
}
