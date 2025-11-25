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
    public class MedicoController : Controller
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IMapper mapper;
        private readonly MedicoBusiness medicoBusiness;

        public MedicoController(IMapper mapper, IHttpContextAccessor contextAccessor, MedicoBusiness medicoBusiness)
        {
            this.mapper = mapper;
            this.contextAccessor = contextAccessor;
            this.medicoBusiness = medicoBusiness;
        }

        [HttpGet()]
        public async Task<ActionResult<IList<MedicoDTO>>> GetAllMedicos()
        {
            try
            {
                var list = await medicoBusiness.GetAsync();
                if (list == null || !list.Any())
                    return NotFound(new ActionResultDTO { Message = "No se encontraron médicos" });

                var dto = mapper.Map<IList<MedicoDTO>>(list);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionResultDTO { Message = $"Error interno: {ex.Message}" });
            }
        }

        [HttpPost()]
        public async Task<ActionResult<ActionResultDTO>> Add([FromBody] MedicoDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new ActionResultDTO { Message = "Datos inválidos" });

                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    return BadRequest(new ActionResultDTO { Message = "El nombre del médico es requerido" });

                if (string.IsNullOrWhiteSpace(dto.Matricula))
                    return BadRequest(new ActionResultDTO { Message = "La matrícula del médico es requerida" });

                var entity = mapper.Map<Medico>(dto);
                var result = await medicoBusiness.SaveAsync(entity);
                var response = new ActionResultDTO { Code = result.ToString() };

                if (result > 0)
                {
                    response.Message = dto.Id > 0 ? "El médico se actualizó correctamente" : "El médico se registró correctamente";
                    return Ok(response);
                }
                else
                {
                    response.Message = "Error al registrar el médico";
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
