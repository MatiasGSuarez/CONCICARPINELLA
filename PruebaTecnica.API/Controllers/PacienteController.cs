using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Business;
using PruebaTecnica.Model.BaseDTO;
using PruebaTecnica.Model.DTO;
using PruebaTecnica.Model.Model;

namespace PruebaTecnica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : Controller
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IMapper mapper;
        private readonly PacienteBusiness pacienteBusiness;

        public PacienteController(IMapper mapper, IHttpContextAccessor contextAccessor, PacienteBusiness pacienteBusiness)
        {
            this.mapper = mapper;
            this.contextAccessor = contextAccessor;
            this.pacienteBusiness = pacienteBusiness;
        }

        [HttpGet()]
        public async Task<ActionResult<IList<PacienteDTO>>> GetAllPacientes()
        {
            try
            {
                var list = await pacienteBusiness.GetAsync();
                if (list == null || !list.Any())
                    return NotFound(new ActionResultDTO { Message = "No se encontraron pacientes" });

                var dto = mapper.Map<IList<PacienteDTO>>(list);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionResultDTO { Message = $"Error interno: {ex.Message}" });
            }
        }

        [HttpPost()]
        public async Task<ActionResult<ActionResultDTO>> Add([FromBody] PacienteDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new ActionResultDTO { Message = "Datos inválidos" });

                if (string.IsNullOrWhiteSpace(dto.Dni))
                    return BadRequest(new ActionResultDTO { Message = "El DNI del paciente es requerido" });

                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    return BadRequest(new ActionResultDTO { Message = "El nombre del paciente es requerido" });

                if (string.IsNullOrWhiteSpace(dto.Apellido))
                    return BadRequest(new ActionResultDTO { Message = "El apellido del paciente es requerido" });

                var entity = mapper.Map<Paciente>(dto);
                var result = await pacienteBusiness.SaveAsync(entity);
                var response = new ActionResultDTO { Code = result.ToString() };

                if (result > 0)
                {
                    response.Message = dto.Id > 0 ? "El paciente se actualizó correctamente" : "El paciente se registró correctamente";
                    return Ok(response);
                }
                else
                {
                    response.Message = "Error al registrar el paciente";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionResultDTO { Message = $"Error interno: {ex.Message}" });
            }
        }
    }
}
