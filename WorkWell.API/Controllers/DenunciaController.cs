using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.OmbudMind;
using WorkWell.Application.Services.OmbudMind;
using WorkWell.API.Security;
using WorkWell.API.SwaggerExamples;

namespace WorkWell.API.Controllers
{
    /// <summary>
    /// Canal OmbudMind - denúncias éticas totalmente confidenciais.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Funcionario", "Admin", "RH")]
    public class DenunciaController : ControllerBase
    {
        private readonly IDenunciaService _denunciaService;

        public DenunciaController(IDenunciaService denunciaService)
        {
            _denunciaService = denunciaService;
        }

        /// <summary>
        /// Lista todas as denúncias.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "Lista de denúncias", typeof(IEnumerable<DenunciaDto>))]
        [ProducesResponseType(typeof(IEnumerable<DenunciaDto>), 200)]
        public async Task<ActionResult<IEnumerable<DenunciaDto>>> GetAll()
        {
            var list = await _denunciaService.GetAllAsync();
            return Ok(list);
        }

        /// <summary>
        /// Busca denúncia por id.
        /// </summary>
        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Denúncia encontrada", typeof(DenunciaDto))]
        [SwaggerResponse(404, "Denúncia não encontrada")]
        [ProducesResponseType(typeof(DenunciaDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<DenunciaDto>> GetById(long id)
        {
            var entity = await _denunciaService.GetByIdAsync(id);
            if (entity == null) return NotFound(new { mensagem = "Denúncia não encontrada." });
            return Ok(entity);
        }

        /// <summary>
        /// Busca denúncia pelo código de rastreamento.
        /// </summary>
        [HttpGet("codigo/{codigo}")]
        [SwaggerResponse(200, "Denúncia encontrada", typeof(DenunciaDto))]
        [SwaggerResponse(404, "Denúncia não encontrada")]
        [ProducesResponseType(typeof(DenunciaDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<DenunciaDto>> GetByCodigoRastreamento(string codigo)
        {
            var entity = await _denunciaService.GetByCodigoRastreamentoAsync(codigo);
            if (entity == null) return NotFound(new { mensagem = "Denúncia não encontrada." });
            return Ok(entity);
        }

        /// <summary>
        /// Cria uma nova denúncia ética.
        /// </summary>
        [HttpPost]
        [SwaggerRequestExample(typeof(DenunciaDto), typeof(DenunciaDtoExample))]
        [SwaggerResponse(201, "Denúncia criada com sucesso", typeof(DenunciaDto))]
        [SwaggerResponse(400, "Dados inválidos")]
        [ProducesResponseType(typeof(DenunciaDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<DenunciaDto>> Create(DenunciaDto dto)
        {
            var id = await _denunciaService.CreateAsync(dto);
            var denunciaCriada = await _denunciaService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, denunciaCriada);
        }

        /// <summary>
        /// Atualiza uma denúncia existente.
        /// </summary>
        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(DenunciaDto), typeof(DenunciaDtoExample))]
        [SwaggerResponse(204, "Denúncia atualizada com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [SwaggerResponse(404, "Denúncia não encontrada")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(long id, DenunciaDto dto)
        {
            if (id != dto.Id) return BadRequest(new { mensagem = "ID da URL e do objeto devem coincidir." });
            var entity = await _denunciaService.GetByIdAsync(id);
            if (entity == null)
                return NotFound(new { mensagem = "Denúncia não encontrada." });

            await _denunciaService.UpdateAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// Remove uma denúncia.
        /// </summary>
        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Denúncia removida com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _denunciaService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Lista as investigações de uma denúncia.
        /// </summary>
        [HttpGet("{denunciaId:long}/investigacoes")]
        [SwaggerResponse(200, "Lista de investigações", typeof(IEnumerable<InvestigacaoDenunciaDto>))]
        [ProducesResponseType(typeof(IEnumerable<InvestigacaoDenunciaDto>), 200)]
        public async Task<ActionResult<IEnumerable<InvestigacaoDenunciaDto>>> GetInvestigacoes(long denunciaId)
        {
            var list = await _denunciaService.GetInvestigacoesAsync(denunciaId);
            return Ok(list);
        }

        /// <summary>
        /// Cria uma nova investigação sobre denúncia.
        /// </summary>
        [HttpPost("{denunciaId:long}/investigacoes")]
        [SwaggerRequestExample(typeof(InvestigacaoDenunciaDto), typeof(InvestigacaoDenunciaDtoExample))]
        [SwaggerResponse(201, "Investigação criada com sucesso", typeof(InvestigacaoDenunciaDto))]
        [ProducesResponseType(typeof(InvestigacaoDenunciaDto), 201)]
        public async Task<ActionResult<InvestigacaoDenunciaDto>> AdicionarInvestigacao(long denunciaId, InvestigacaoDenunciaDto dto)
        {
            var id = await _denunciaService.AdicionarInvestigacaoAsync(denunciaId, dto);
            var investigacaoCriada = (await _denunciaService.GetInvestigacoesAsync(denunciaId)).FirstOrDefault(x => x.Id == id);
            return CreatedAtAction(nameof(GetInvestigacoes), new { denunciaId }, investigacaoCriada);
        }

        /// <summary>
        /// Atualiza uma investigação sobre denúncia.
        /// </summary>
        [HttpPut("{denunciaId:long}/investigacoes/{investigacaoId:long}")]
        [SwaggerRequestExample(typeof(InvestigacaoDenunciaDto), typeof(InvestigacaoDenunciaDtoExample))]
        [SwaggerResponse(204, "Investigação atualizada com sucesso")]
        [SwaggerResponse(400, "ID inconsistente")]
        [SwaggerResponse(404, "Investigação não encontrada")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateInvestigacao(long denunciaId, long investigacaoId, InvestigacaoDenunciaDto dto)
        {
            if (investigacaoId != dto.Id)
                return BadRequest(new { mensagem = "ID da investigação da URL e do objeto devem coincidir." });

            // Service deve verificar se pertence à denúncia
            var atualizou = await _denunciaService.UpdateInvestigacaoAsync(denunciaId, dto);
            if (!atualizou)
                return NotFound(new { mensagem = "Investigação não encontrada para a denúncia informada." });

            return NoContent();
        }

        /// <summary>
        /// Remove uma investigação sobre denúncia.
        /// </summary>
        [HttpDelete("{denunciaId:long}/investigacoes/{investigacaoId:long}")]
        [SwaggerResponse(204, "Investigação removida com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteInvestigacao(long denunciaId, long investigacaoId)
        {
            var removido = await _denunciaService.DeleteInvestigacaoAsync(denunciaId, investigacaoId);
            if (!removido)
                return NotFound(new { mensagem = "Investigação não encontrada para a denúncia informada." });
            return NoContent();
        }
    }
}