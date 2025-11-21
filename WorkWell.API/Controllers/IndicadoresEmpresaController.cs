using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WorkWell.API.Helpers;
using WorkWell.API.Security;
using WorkWell.API.SwaggerExamples;
using WorkWell.Application.DTOs.Indicadores;
using WorkWell.Application.DTOs.Paginacao;
using WorkWell.Application.Services.Indicadores;

namespace WorkWell.API.Controllers
{
    /// <summary>
    /// Indicadores de clima do RH (média de humor, adesão a eventos, frequência em consultas etc).
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Admin", "RH")]
    public class IndicadoresEmpresaController : ControllerBase
    {
        private readonly IIndicadoresEmpresaService _indicadoresService;

        public IndicadoresEmpresaController(IIndicadoresEmpresaService indicadoresService)
        {
            _indicadoresService = indicadoresService;
        }

        /// <summary>
        /// Lista paginada de indicadores registrados.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "Lista paginada de indicadores", typeof(PagedResultDto<IndicadoresEmpresaDto>))]
        [ProducesResponseType(typeof(PagedResultDto<IndicadoresEmpresaDto>), 200)]
        public async Task<ActionResult<PagedResultDto<IndicadoresEmpresaDto>>> GetAllPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _indicadoresService.GetAllPagedAsync(page, pageSize);
            result.Links = HateoasLinkBuilder.BuildPagedLinks(HttpContext, result.Page, result.PageSize, result.TotalCount);
            return Ok(result);
        }

        /// <summary>
        /// Busca indicadores por Id.
        /// </summary>
        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Indicadores da empresa encontrados", typeof(IndicadoresEmpresaDto))]
        [SwaggerResponse(404, "Registro não encontrado")]
        [ProducesResponseType(typeof(IndicadoresEmpresaDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IndicadoresEmpresaDto>> GetById(long id)
        {
            var entity = await _indicadoresService.GetByIdAsync(id);
            if (entity == null)
                return NotFound(new { mensagem = "Registro de indicadores não encontrado." });
            return Ok(entity);
        }

        /// <summary>
        /// Cadastra novos indicadores para empresa.
        /// </summary>
        [HttpPost]
        [SwaggerRequestExample(typeof(IndicadoresEmpresaDto), typeof(IndicadoresEmpresaDtoExample))]
        [SwaggerResponse(201, "Indicadores criados com sucesso", typeof(IndicadoresEmpresaDto))]
        [ProducesResponseType(typeof(IndicadoresEmpresaDto), 201)]
        public async Task<ActionResult<IndicadoresEmpresaDto>> Create(IndicadoresEmpresaDto dto)
        {
            var id = await _indicadoresService.CreateAsync(dto);
            var entityCriada = await _indicadoresService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, entityCriada);
        }

        /// <summary>
        /// Atualiza indicadores existentes.
        /// </summary>
        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(IndicadoresEmpresaDto), typeof(IndicadoresEmpresaDtoExample))]
        [SwaggerResponse(204, "Indicadores atualizados com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(long id, IndicadoresEmpresaDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { mensagem = "ID da URL e do objeto devem coincidir." });

            await _indicadoresService.UpdateAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// Remove registro de indicadores.
        /// </summary>
        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Indicadores removidos")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _indicadoresService.DeleteAsync(id);
            return NoContent();
        }
    }
}