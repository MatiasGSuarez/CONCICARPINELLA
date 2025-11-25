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
    public class PrestadorController : Controller
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IMapper mapper;
        private readonly PrestadorBusiness prestadorBusiness;

        public PrestadorController(IMapper mapper, IHttpContextAccessor contextAccessor, PrestadorBusiness prestadorBusiness)
        {
            this.mapper = mapper;
            this.contextAccessor = contextAccessor;
            this.prestadorBusiness = prestadorBusiness;
        }

        [HttpGet()]
        public async Task<ActionResult<IList<PrestadorDTO>>> GetAllPrestadores()
        {
            try
            {
                var list = await prestadorBusiness.GetAsync();
                if (list == null || !list.Any())
                    return NotFound(new ActionResultDTO { Message = "No se encontraron prestadores" });

                var dto = mapper.Map<IList<PrestadorDTO>>(list);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionResultDTO { Message = $"Error interno: {ex.Message}" });
            }
        }

         [HttpPost()]
        public async Task<ActionResult<ActionResultDTO>> Add([FromBody] PrestadorDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new ActionResultDTO { Message = "Datos inválidos" });

                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    return BadRequest(new ActionResultDTO { Message = "El nombre del prestador es requerido" });

                var entity = mapper.Map<Prestador>(dto);
                var result = await prestadorBusiness.SaveAsync(entity);
                var response = new ActionResultDTO { Code = result.ToString() };

                if (result > 0)
                {
                    response.Message = dto.Id > 0 ? "El prestador se actualizó correctamente" : "El prestador se registró correctamente";
                    return Ok(response);
                }
                else
                {
                    response.Message = "Error al registrar el prestador";
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