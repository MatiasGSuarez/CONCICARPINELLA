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
    public class EstudioController : Controller
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IMapper mapper;
        private readonly EstudioBusiness estudioBusiness;

        public EstudioController(IMapper mapper, IHttpContextAccessor contextAccessor, EstudioBusiness estudioBusiness)
        {
            this.mapper = mapper;
            this.contextAccessor = contextAccessor;
            this.estudioBusiness = estudioBusiness;
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

        [HttpPost()]
        public async Task<ActionResult<ActionResultDTO>> Add([FromBody] EstudioDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new ActionResultDTO { Message = "Datos inválidos" });

                if (string.IsNullOrWhiteSpace(dto.Codigo))
                    return BadRequest(new ActionResultDTO { Message = "El código del estudio es requerido" });

                if (string.IsNullOrWhiteSpace(dto.Descripcion))
                    return BadRequest(new ActionResultDTO { Message = "La descripción del estudio es requerida" });

                if (dto.PacienteId <= 0)
                    return BadRequest(new ActionResultDTO { Message = "El ID del paciente es requerido" });

                if (dto.MedicoId <= 0)
                    return BadRequest(new ActionResultDTO { Message = "El ID del médico es requerido" });

                if (dto.PrestadorId <= 0)
                    return BadRequest(new ActionResultDTO { Message = "El ID del prestador es requerido" });

                var entity = mapper.Map<Estudio>(dto);
                var result = await estudioBusiness.SaveAsync(entity);
                var response = new ActionResultDTO { Code = result.ToString() };

                if (result > 0)
                {
                    response.Message = dto.Id > 0 ? "El estudio se actualizó correctamente" : "El estudio se registró correctamente";
                    return Ok(response);
                }
                else
                {
                    response.Message = "Error al registrar el estudio";
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
