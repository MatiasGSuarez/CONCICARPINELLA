using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.API.Common;
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

        [HttpPost()]
        public async Task<ActionResult<PacienteDTO>> CreatePaciente([FromBody] PacienteDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new ActionResultDTO { Message = "Datos inválidos" });

                var paciente = mapper.Map<Paciente>(dto);
                var idpac = await pacienteBusiness.SaveAsync(paciente);
                dto.Id = idpac;
                return Ok(dto);
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

    }
}
