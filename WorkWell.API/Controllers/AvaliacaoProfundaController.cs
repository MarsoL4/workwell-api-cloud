using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using WorkWell.Application.Services.AvaliacoesEmocionais;
using WorkWell.API.Security;
using WorkWell.API.SwaggerExamples;

namespace WorkWell.API.Controllers
{
    /// <summary>
    /// Gerencia avaliações emocionais profundas para funcionários (escala GAD-7, PHQ-9, etc).
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Funcionario", "Psicologo", "RH", "Admin")]
    public class AvaliacaoProfundaController : ControllerBase
    {
        private readonly IAvaliacaoProfundaService _avaliacaoService;

        public AvaliacaoProfundaController(IAvaliacaoProfundaService avaliacaoService)
        {
            _avaliacaoService = avaliacaoService;
        }

        /// <summary>
        /// Lista todas as avaliações profundas registradas.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "Lista de avaliações profundas", typeof(IEnumerable<AvaliacaoProfundaDto>))]
        [ProducesResponseType(typeof(IEnumerable<AvaliacaoProfundaDto>), 200)]
        public async Task<ActionResult<IEnumerable<AvaliacaoProfundaDto>>> GetAll()
        {
            var registros = await _avaliacaoService.GetAllAsync();
            return Ok(registros);
        }

        /// <summary>
        /// Busca uma avaliação profunda pelo ID.
        /// </summary>
        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Avaliação profunda encontrada", typeof(AvaliacaoProfundaDto))]
        [SwaggerResponse(404, "Avaliação não encontrada")]
        [ProducesResponseType(typeof(AvaliacaoProfundaDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AvaliacaoProfundaDto>> GetById(long id)
        {
            var entity = await _avaliacaoService.GetByIdAsync(id);
            if (entity == null)
                return NotFound(new { mensagem = "Avaliação não encontrada." });
            return Ok(entity);
        }

        /// <summary>
        /// Registra uma avaliação emocional profunda.
        /// </summary>
        /// <remarks>
        /// O campo <b>Id</b> é gerado automaticamente.
        /// </remarks>
        [HttpPost]
        [SwaggerRequestExample(typeof(AvaliacaoProfundaDto), typeof(AvaliacaoProfundaDtoExample))]
        [SwaggerResponse(201, "Avaliação criada com sucesso", typeof(AvaliacaoProfundaDto))]
        [SwaggerResponse(400, "Dados inválidos")]
        [ProducesResponseType(typeof(AvaliacaoProfundaDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<AvaliacaoProfundaDto>> Create(AvaliacaoProfundaDto dto)
        {
            var id = await _avaliacaoService.CreateAsync(dto);
            var entityCriada = await _avaliacaoService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, entityCriada);
        }

        /// <summary>
        /// Atualiza uma avaliação profunda existente.
        /// </summary>
        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(AvaliacaoProfundaDto), typeof(AvaliacaoProfundaDtoExample))]
        [SwaggerResponse(204, "Avaliação atualizada com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [SwaggerResponse(404, "Avaliação não encontrada")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(long id, AvaliacaoProfundaDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { mensagem = "ID da URL e do objeto devem coincidir." });

            var entity = await _avaliacaoService.GetByIdAsync(id);
            if (entity == null)
                return NotFound(new { mensagem = "Avaliação não encontrada." });

            await _avaliacaoService.UpdateAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// Remove uma avaliação profunda.
        /// </summary>
        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Avaliação removida com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _avaliacaoService.DeleteAsync(id);
            return NoContent();
        }
    }
}