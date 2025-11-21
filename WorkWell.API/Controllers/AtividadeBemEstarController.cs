using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.AtividadesBemEstar;
using WorkWell.Application.Services.AtividadesBemEstar;
using WorkWell.API.Security;
using WorkWell.API.SwaggerExamples;

namespace WorkWell.API.Controllers
{
    /// <summary>
    /// Gerencia as atividades de bem-estar promovidas pela empresa.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Admin", "RH")]
    public class AtividadeBemEstarController : ControllerBase
    {
        private readonly IAtividadeBemEstarService _atividadeService;

        public AtividadeBemEstarController(IAtividadeBemEstarService atividadeService)
        {
            _atividadeService = atividadeService;
        }

        /// <summary>
        /// Lista todas as atividades de bem-estar cadastradas.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "Lista de atividades", typeof(IEnumerable<AtividadeBemEstarDto>))]
        [ProducesResponseType(typeof(IEnumerable<AtividadeBemEstarDto>), 200)]
        public async Task<ActionResult<IEnumerable<AtividadeBemEstarDto>>> GetAll()
        {
            var atividades = await _atividadeService.GetAllAsync();
            return Ok(atividades);
        }

        /// <summary>
        /// Busca uma atividade pelo ID.
        /// </summary>
        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Atividade encontrada", typeof(AtividadeBemEstarDto))]
        [SwaggerResponse(404, "Atividade não encontrada")]
        [ProducesResponseType(typeof(AtividadeBemEstarDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AtividadeBemEstarDto>> GetById(long id)
        {
            var atividade = await _atividadeService.GetByIdAsync(id);
            if (atividade == null)
                return NotFound(new { mensagem = "Atividade não encontrada." });
            return Ok(atividade);
        }

        /// <summary>
        /// Cadastra uma nova atividade de bem-estar.
        /// </summary>
        /// <remarks>
        /// O campo <b>Id</b> é gerado automaticamente. Informe empresa, tipo, datas, setor opcionalmente, etc.
        /// </remarks>
        [HttpPost]
        [SwaggerRequestExample(typeof(AtividadeBemEstarDto), typeof(AtividadeBemEstarDtoExample))]
        [SwaggerResponse(201, "Atividade criada com sucesso", typeof(AtividadeBemEstarDto))]
        [SwaggerResponse(400, "Dados inválidos")]
        [ProducesResponseType(typeof(AtividadeBemEstarDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<AtividadeBemEstarDto>> Create(AtividadeBemEstarDto dto)
        {
            var id = await _atividadeService.CreateAsync(dto);
            var atividadeCriada = await _atividadeService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, atividadeCriada);
        }

        /// <summary>
        /// Atualiza uma atividade existente.
        /// </summary>
        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(AtividadeBemEstarDto), typeof(AtividadeBemEstarDtoExample))]
        [SwaggerResponse(204, "Atividade atualizada com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [SwaggerResponse(404, "Atividade não encontrada")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(long id, AtividadeBemEstarDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { mensagem = "ID da URL e do objeto devem coincidir." });

            var atividade = await _atividadeService.GetByIdAsync(id);
            if (atividade == null)
                return NotFound(new { mensagem = "Atividade não encontrada." });

            await _atividadeService.UpdateAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// Remove uma atividade de bem-estar.
        /// </summary>
        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Atividade removida com sucesso")]
        [SwaggerResponse(404, "Atividade não encontrada")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(long id)
        {
            var atividade = await _atividadeService.GetByIdAsync(id);
            if (atividade == null)
                return NotFound(new { mensagem = "Atividade não encontrada." });
            await _atividadeService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Lista as participações de funcionários em uma atividade.
        /// </summary>
        [HttpGet("{atividadeId:long}/participacoes")]
        [SwaggerResponse(200, "Lista de participações", typeof(IEnumerable<ParticipacaoAtividadeDto>))]
        [ProducesResponseType(typeof(IEnumerable<ParticipacaoAtividadeDto>), 200)]
        public async Task<ActionResult<IEnumerable<ParticipacaoAtividadeDto>>> GetParticipacoes(long atividadeId)
        {
            var participacoes = await _atividadeService.GetParticipacoesAsync(atividadeId);
            return Ok(participacoes);
        }

        /// <summary>
        /// Registra participação de um funcionário em uma atividade.
        /// </summary>
        /// <remarks>
        /// Informe AtividadeId (na URL), FuncionarioId e outros campos necessários.
        /// </remarks>
        [HttpPost("{atividadeId:long}/participacoes")]
        [SwaggerRequestExample(typeof(ParticipacaoAtividadeDto), typeof(ParticipacaoAtividadeDtoExample))]
        [SwaggerResponse(200, "Participação registrada com sucesso", typeof(ParticipacaoAtividadeDto))]
        [ProducesResponseType(typeof(ParticipacaoAtividadeDto), 200)]
        public async Task<ActionResult<ParticipacaoAtividadeDto>> Participar(long atividadeId, ParticipacaoAtividadeDto dto)
        {
            var id = await _atividadeService.AdicionarParticipacaoAsync(atividadeId, dto);
            var participacaoCriada = (await _atividadeService.GetParticipacoesAsync(atividadeId)).FirstOrDefault(x => x.Id == id);
            return Ok(participacaoCriada);
        }

        /// <summary>
        /// Atualiza a participação de um funcionário em uma atividade.
        /// </summary>
        [HttpPut("{atividadeId:long}/participacoes/{participacaoId:long}")]
        [SwaggerRequestExample(typeof(ParticipacaoAtividadeDto), typeof(ParticipacaoAtividadeDtoExample))]
        [SwaggerResponse(204, "Participação atualizada com sucesso")]
        [SwaggerResponse(400, "ID inconsistente")]
        [SwaggerResponse(404, "Participação não encontrada")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateParticipacao(long atividadeId, long participacaoId, ParticipacaoAtividadeDto dto)
        {
            if (participacaoId != dto.Id)
                return BadRequest(new { mensagem = "ID da participação (URL e objeto) devem coincidir." });

            var atualizado = await _atividadeService.UpdateParticipacaoAsync(atividadeId, dto);
            if (!atualizado)
                return NotFound(new { mensagem = "Participação não encontrada para a atividade informada." });
            return NoContent();
        }

        /// <summary>
        /// Remove a participação de um funcionário em uma atividade.
        /// </summary>
        [HttpDelete("{atividadeId:long}/participacoes/{participacaoId:long}")]
        [SwaggerResponse(204, "Participação removida com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteParticipacao(long atividadeId, long participacaoId)
        {
            var removido = await _atividadeService.DeleteParticipacaoAsync(atividadeId, participacaoId);
            if (!removido)
                return NotFound(new { mensagem = "Participação não encontrada para a atividade informada." });
            return NoContent();
        }
    }
}