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

    }
}
