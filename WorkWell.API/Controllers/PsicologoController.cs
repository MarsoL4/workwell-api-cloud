using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WorkWell.API.Security;
using WorkWell.API.SwaggerExamples;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Application.DTOs.Paginacao;
using WorkWell.Application.Services.ApoioPsicologico;

namespace WorkWell.API.Controllers
{
    /// <summary>
    /// Cadastro de psicólogos habilitados para atendimento corporativo.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Admin", "RH")]
    public class PsicologoController : ControllerBase
    {
        private readonly IPsicologoService _psicologoService;

        public PsicologoController(IPsicologoService psicologoService)
        {
            _psicologoService = psicologoService;
        }

        [HttpGet]
        [SwaggerResponse(200, "Lista paginada de psicólogos", typeof(PagedResultDto<PsicologoDto>))]
        [ProducesResponseType(typeof(PagedResultDto<PsicologoDto>), 200)]
        public async Task<ActionResult<PagedResultDto<PsicologoDto>>> GetAllPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _psicologoService.GetAllPagedAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Psicólogo encontrado", typeof(PsicologoDto))]
        [SwaggerResponse(404, "Psicólogo não encontrado")]
        [ProducesResponseType(typeof(PsicologoDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PsicologoDto>> GetById(long id)
        {
            var psicologo = await _psicologoService.GetByIdAsync(id);
            if (psicologo == null)
                return NotFound(new { mensagem = "Psicólogo não encontrado." });
            return Ok(psicologo);
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(PsicologoDto), typeof(PsicologoDtoExample))]
        [SwaggerResponse(201, "Psicólogo criado com sucesso", typeof(PsicologoDto))]
        [ProducesResponseType(typeof(PsicologoDto), 201)]
        public async Task<ActionResult<PsicologoDto>> Create(PsicologoDto dto)
        {
            var id = await _psicologoService.CreateAsync(dto);
            var psicologoCriado = await _psicologoService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, psicologoCriado);
        }

        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(PsicologoDto), typeof(PsicologoDtoExample))]
        [SwaggerResponse(204, "Psicólogo atualizado com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(long id, PsicologoDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { mensagem = "ID da URL e do objeto devem coincidir." });

            await _psicologoService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Psicólogo removido com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _psicologoService.DeleteAsync(id);
            return NoContent();
        }
    }
}