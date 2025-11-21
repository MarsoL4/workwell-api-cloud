using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WorkWell.API.Security;
using WorkWell.API.SwaggerExamples;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Application.Services.ApoioPsicologico;

namespace WorkWell.API.Controllers
{
    /// <summary>
    /// SOS Emocional: botão de emergência (funcionários) e gestão dos eventos (psicólogos e RH).
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Funcionario", "Psicologo", "RH")]
    public class SOSemergenciaController : ControllerBase
    {
        private readonly ISOSemergenciaService _sosService;

        public SOSemergenciaController(ISOSemergenciaService sosService)
        {
            _sosService = sosService;
        }

        [HttpGet]
        [SwaggerResponse(200, "Lista de casos de SOS/emergência", typeof(IEnumerable<SOSemergenciaDto>))]
        [ProducesResponseType(typeof(IEnumerable<SOSemergenciaDto>), 200)]
        public async Task<ActionResult<IEnumerable<SOSemergenciaDto>>> GetAll()
        {
            var registros = await _sosService.GetAllAsync();
            return Ok(registros);
        }

        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Registro encontrado", typeof(SOSemergenciaDto))]
        [SwaggerResponse(404, "Registro não encontrado")]
        [ProducesResponseType(typeof(SOSemergenciaDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SOSemergenciaDto>> GetById(long id)
        {
            var entidade = await _sosService.GetByIdAsync(id);
            if (entidade == null)
                return NotFound(new { mensagem = "Registro não encontrado." });
            return Ok(entidade);
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(SOSemergenciaDto), typeof(SOSemergenciaDtoExample))]
        [SwaggerResponse(201, "SOS registrado com sucesso", typeof(SOSemergenciaDto))]
        [ProducesResponseType(typeof(SOSemergenciaDto), 201)]
        public async Task<ActionResult<SOSemergenciaDto>> Create(SOSemergenciaDto dto)
        {
            var id = await _sosService.CreateAsync(dto);
            var sosCriado = await _sosService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, sosCriado);
        }

        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(SOSemergenciaDto), typeof(SOSemergenciaDtoExample))]
        [SwaggerResponse(204, "Registro atualizado com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(long id, SOSemergenciaDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { mensagem = "ID da URL e do objeto devem coincidir." });

            await _sosService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Registro removido com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _sosService.DeleteAsync(id);
            return NoContent();
        }
    }
}