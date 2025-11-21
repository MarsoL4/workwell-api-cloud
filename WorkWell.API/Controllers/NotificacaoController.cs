using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WorkWell.API.Helpers;
using WorkWell.API.Security;
using WorkWell.API.SwaggerExamples;
using WorkWell.Application.DTOs.Notificacoes;
using WorkWell.Application.DTOs.Paginacao;
using WorkWell.Application.Services.Notificacoes;

namespace WorkWell.API.Controllers
{
    /// <summary>
    /// Sistema de notificações inteligentes para funcionários e RH.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Admin", "RH", "Funcionario")]
    public class NotificacaoController : ControllerBase
    {
        private readonly INotificacaoService _notificacaoService;

        public NotificacaoController(INotificacaoService notificacaoService)
        {
            _notificacaoService = notificacaoService;
        }

        [HttpGet]
        [SwaggerResponse(200, "Lista paginada de notificações", typeof(PagedResultDto<NotificacaoDto>))]
        [ProducesResponseType(typeof(PagedResultDto<NotificacaoDto>), 200)]
        public async Task<ActionResult<PagedResultDto<NotificacaoDto>>> GetAllPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _notificacaoService.GetAllPagedAsync(page, pageSize);
            result.Links = HateoasLinkBuilder.BuildPagedLinks(HttpContext, result.Page, result.PageSize, result.TotalCount);
            return Ok(result);
        }

        [HttpGet("funcionario/{funcionarioId:long}")]
        [SwaggerResponse(200, "Notificações para funcionário", typeof(IEnumerable<NotificacaoDto>))]
        [ProducesResponseType(typeof(IEnumerable<NotificacaoDto>), 200)]
        public async Task<ActionResult<IEnumerable<NotificacaoDto>>> GetByFuncionario(long funcionarioId)
        {
            var notificacoes = await _notificacaoService.GetAllByFuncionarioIdAsync(funcionarioId);
            return Ok(notificacoes);
        }

        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Notificação encontrada", typeof(NotificacaoDto))]
        [SwaggerResponse(404, "Notificação não encontrada")]
        [ProducesResponseType(typeof(NotificacaoDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<NotificacaoDto>> GetById(long id)
        {
            var notificacao = await _notificacaoService.GetByIdAsync(id);
            if (notificacao == null)
                return NotFound(new { mensagem = "Notificação não encontrada." });
            return Ok(notificacao);
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(NotificacaoDto), typeof(NotificacaoDtoExample))]
        [SwaggerResponse(201, "Notificação criada com sucesso", typeof(NotificacaoDto))]
        [ProducesResponseType(typeof(NotificacaoDto), 201)]
        public async Task<ActionResult<NotificacaoDto>> Create(NotificacaoDto dto)
        {
            var id = await _notificacaoService.CreateAsync(dto);
            var notificacaoCriada = await _notificacaoService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, notificacaoCriada);
        }

        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(NotificacaoDto), typeof(NotificacaoDtoExample))]
        [SwaggerResponse(204, "Notificação atualizada com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(long id, NotificacaoDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { mensagem = "ID da URL e do objeto devem coincidir." });

            await _notificacaoService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Notificação removida com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _notificacaoService.DeleteAsync(id);
            return NoContent();
        }
    }
}