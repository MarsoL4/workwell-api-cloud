using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WorkWell.API.Security;
using WorkWell.API.SwaggerExamples;
using WorkWell.Application.DTOs.Enquetes;
using WorkWell.Application.DTOs.Paginacao;
using WorkWell.Application.Services.Enquetes;

namespace WorkWell.API.Controllers
{
    /// <summary>
    /// Gerencia as enquetes corporativas rápidas para acompanhamento de clima organizacional.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Admin", "RH")]
    public class EnqueteController : ControllerBase
    {
        private readonly IEnqueteService _enqueteService;

        public EnqueteController(IEnqueteService enqueteService)
        {
            _enqueteService = enqueteService;
        }

        /// <summary>
        /// Lista enquetes paginadas.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "Lista paginada de enquetes", typeof(PagedResultDto<EnqueteDto>))]
        [ProducesResponseType(typeof(PagedResultDto<EnqueteDto>), 200)]
        public async Task<ActionResult<PagedResultDto<EnqueteDto>>> GetAllPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _enqueteService.GetAllPagedAsync(page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Busca enquete por Id.
        /// </summary>
        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Enquete encontrada", typeof(EnqueteDto))]
        [SwaggerResponse(404, "Enquete não encontrada")]
        [ProducesResponseType(typeof(EnqueteDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<EnqueteDto>> GetById(long id)
        {
            var enquete = await _enqueteService.GetByIdAsync(id);
            if (enquete == null)
                return NotFound(new { mensagem = "Enquete não encontrada." });
            return Ok(enquete);
        }

        /// <summary>
        /// Cria uma nova enquete.
        /// </summary>
        [HttpPost]
        [SwaggerRequestExample(typeof(EnqueteDto), typeof(EnqueteDtoExample))]
        [SwaggerResponse(201, "Enquete criada com sucesso", typeof(EnqueteDto))]
        [ProducesResponseType(typeof(EnqueteDto), 201)]
        public async Task<ActionResult<EnqueteDto>> Create(EnqueteDto dto)
        {
            var id = await _enqueteService.CreateAsync(dto);
            var enqueteCriada = await _enqueteService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, enqueteCriada);
        }

        /// <summary>
        /// Atualiza uma enquete existente.
        /// </summary>
        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(EnqueteDto), typeof(EnqueteDtoExample))]
        [SwaggerResponse(204, "Enquete atualizada com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(long id, EnqueteDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { mensagem = "ID da URL e do objeto devem coincidir." });
            await _enqueteService.UpdateAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// Remove uma enquete.
        /// </summary>
        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Enquete removida com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _enqueteService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Lista as respostas de uma enquete.
        /// </summary>
        [HttpGet("{enqueteId:long}/respostas")]
        [SwaggerResponse(200, "Respostas da enquete", typeof(IEnumerable<RespostaEnqueteDto>))]
        [ProducesResponseType(typeof(IEnumerable<RespostaEnqueteDto>), 200)]
        public async Task<ActionResult<IEnumerable<RespostaEnqueteDto>>> GetRespostas(long enqueteId)
        {
            var respostas = await _enqueteService.GetRespostasAsync(enqueteId);
            return Ok(respostas);
        }

        /// <summary>
        /// Adiciona resposta a uma enquete.
        /// </summary>
        [HttpPost("{enqueteId:long}/respostas")]
        [SwaggerRequestExample(typeof(RespostaEnqueteDto), typeof(RespostaEnqueteDtoExample))]
        [SwaggerResponse(201, "Resposta registrada com sucesso", typeof(RespostaEnqueteDto))]
        [ProducesResponseType(typeof(RespostaEnqueteDto), 201)]
        public async Task<ActionResult<RespostaEnqueteDto>> AdicionarResposta(long enqueteId, RespostaEnqueteDto dto)
        {
            var id = await _enqueteService.AdicionarRespostaAsync(enqueteId, dto);
            var respostaCriada = (await _enqueteService.GetRespostasAsync(enqueteId)).FirstOrDefault(r => r.Id == id);
            return CreatedAtAction(nameof(GetRespostas), new { enqueteId }, respostaCriada);
        }

        /// <summary>
        /// Atualiza uma resposta de enquete.
        /// </summary>
        [HttpPut("{enqueteId:long}/respostas/{respostaId:long}")]
        [SwaggerRequestExample(typeof(RespostaEnqueteDto), typeof(RespostaEnqueteDtoExample))]
        [SwaggerResponse(204, "Resposta atualizada com sucesso")]
        [SwaggerResponse(400, "ID inconsistente")]
        [SwaggerResponse(404, "Resposta não encontrada")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateResposta(long enqueteId, long respostaId, RespostaEnqueteDto dto)
        {
            if (respostaId != dto.Id)
                return BadRequest(new { mensagem = "ID da resposta da URL e do objeto devem coincidir." });

            var atualizou = await _enqueteService.UpdateRespostaAsync(enqueteId, dto);
            if (!atualizou)
                return NotFound(new { mensagem = "Resposta não encontrada para a enquete informada." });

            return NoContent();
        }

        /// <summary>
        /// Remove uma resposta de enquete.
        /// </summary>
        [HttpDelete("{enqueteId:long}/respostas/{respostaId:long}")]
        [SwaggerResponse(204, "Resposta removida com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteResposta(long enqueteId, long respostaId)
        {
            var removido = await _enqueteService.DeleteRespostaAsync(enqueteId, respostaId);
            if (!removido)
                return NotFound(new { mensagem = "Resposta não encontrada para a enquete informada." });
            return NoContent();
        }
    }
}