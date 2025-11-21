using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WorkWell.API.Security;
using WorkWell.API.SwaggerExamples;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using WorkWell.Application.Services.AvaliacoesEmocionais;

namespace WorkWell.API.Controllers
{
    /// <summary>
    /// Registros de risco psicossocial com base em autoavaliação e análise do RH.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Funcionario", "RH", "Psicologo")]
    public class RiscoPsicossocialController : ControllerBase
    {
        private readonly IRiscoPsicossocialService _riscoService;

        public RiscoPsicossocialController(IRiscoPsicossocialService riscoService)
        {
            _riscoService = riscoService;
        }

        [HttpGet]
        [SwaggerResponse(200, "Lista de riscos psicossociais", typeof(IEnumerable<RiscoPsicossocialDto>))]
        [ProducesResponseType(typeof(IEnumerable<RiscoPsicossocialDto>), 200)]
        public async Task<ActionResult<IEnumerable<RiscoPsicossocialDto>>> GetAll()
        {
            var registros = await _riscoService.GetAllAsync();
            return Ok(registros);
        }

        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Registro encontrado", typeof(RiscoPsicossocialDto))]
        [SwaggerResponse(404, "Registro não encontrado")]
        [ProducesResponseType(typeof(RiscoPsicossocialDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RiscoPsicossocialDto>> GetById(long id)
        {
            var entity = await _riscoService.GetByIdAsync(id);
            if (entity == null)
                return NotFound(new { mensagem = "Registro não encontrado." });
            return Ok(entity);
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(RiscoPsicossocialDto), typeof(RiscoPsicossocialDtoExample))]
        [SwaggerResponse(201, "Registro criado com sucesso", typeof(RiscoPsicossocialDto))]
        [ProducesResponseType(typeof(RiscoPsicossocialDto), 201)]
        public async Task<ActionResult<RiscoPsicossocialDto>> Create(RiscoPsicossocialDto dto)
        {
            var id = await _riscoService.CreateAsync(dto);
            var registroCriado = await _riscoService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, registroCriado);
        }

        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(RiscoPsicossocialDto), typeof(RiscoPsicossocialDtoExample))]
        [SwaggerResponse(204, "Registro atualizado com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(long id, RiscoPsicossocialDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { mensagem = "ID da URL e do objeto devem coincidir." });

            await _riscoService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Registro removido com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _riscoService.DeleteAsync(id);
            return NoContent();
        }
    }
}