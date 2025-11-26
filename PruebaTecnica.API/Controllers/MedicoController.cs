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
        public async Task<ActionResult<MedicoDTO>> CreateMedico([FromBody] MedicoDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new ActionResultDTO { Message = "Datos inválidos" });

                var medico = await medicoBusiness.CreateMedicoAsync(dto);
                var medicoDto = mapper.Map<MedicoDTO>(medico);

                return Ok(medicoDto);
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

        [HttpPut("{matricula}")]
        public async Task<ActionResult<MedicoDTO>> UpdateMedicobyMatricula(string matricula, [FromBody] MedicoDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new ActionResultDTO { Message = "Datos inválidos" });

                var medico = await medicoBusiness.UpdateMedicobyMatriculaAsync(matricula, dto);
                var medicoDto = mapper.Map<MedicoDTO>(medico);

                return Ok(medicoDto);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<ActionResultDTO>> DeleteMedico(int id)
        {
            try
            {
                var result = await medicoBusiness.DeleteAsync(id);

                if (result)
                    return Ok(new ActionResultDTO { Code = "200", Message = "El médico se eliminó correctamente" });
                else
                    return NotFound(new ActionResultDTO { Message = $"No se encontró un médico con el ID {id}" });
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
    }
}
