using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Application.Services.ApoioPsicologico;
using WorkWell.API.Security;
using WorkWell.API.SwaggerExamples;

namespace WorkWell.API.Controllers
{
    /// <summary>
    /// Canal 100% sigiloso para comunicação entre funcionário e psicólogo.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Funcionario", "Psicologo", "RH", "Admin")]
    public class ChatAnonimoController : ControllerBase
    {
        private readonly IChatAnonimoService _chatService;

        public ChatAnonimoController(IChatAnonimoService chatService)
        {
            _chatService = chatService;
        }

        /// <summary>
        /// Lista todas as conversas anônimas.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "Lista de chats", typeof(IEnumerable<ChatAnonimoDto>))]
        [ProducesResponseType(typeof(IEnumerable<ChatAnonimoDto>), 200)]
        public async Task<ActionResult<IEnumerable<ChatAnonimoDto>>> GetAll()
        {
            var chats = await _chatService.GetAllAsync();
            return Ok(chats);
        }

        /// <summary>
        /// Busca uma conversa pelo id.
        /// </summary>
        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Chat encontrado", typeof(ChatAnonimoDto))]
        [SwaggerResponse(404, "Chat não encontrado")]
        [ProducesResponseType(typeof(ChatAnonimoDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ChatAnonimoDto>> GetById(long id)
        {
            var chat = await _chatService.GetByIdAsync(id);
            if (chat == null)
                return NotFound(new { mensagem = "Chat não encontrado." });
            return Ok(chat);
        }

        /// <summary>
        /// Cria uma nova conversa anônima.
        /// </summary>
        /// <remarks>
        /// Funcionário pode ser anônimo (RemetenteId pode ser null).
        /// </remarks>
        [HttpPost]
        [SwaggerRequestExample(typeof(ChatAnonimoDto), typeof(ChatAnonimoDtoExample))]
        [SwaggerResponse(201, "Chat criado com sucesso", typeof(ChatAnonimoDto))]
        [SwaggerResponse(400, "Dados inválidos")]
        [ProducesResponseType(typeof(ChatAnonimoDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ChatAnonimoDto>> Create(ChatAnonimoDto dto)
        {
            var id = await _chatService.CreateAsync(dto);
            var chatCriado = await _chatService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, chatCriado);
        }

        /// <summary>
        /// Atualiza uma conversa.
        /// </summary>
        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(ChatAnonimoDto), typeof(ChatAnonimoDtoExample))]
        [SwaggerResponse(204, "Chat atualizado com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [SwaggerResponse(404, "Chat não encontrado")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(long id, ChatAnonimoDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { mensagem = "ID da URL e do objeto devem coincidir." });

            var chat = await _chatService.GetByIdAsync(id);
            if (chat == null)
                return NotFound(new { mensagem = "Chat não encontrado." });

            await _chatService.UpdateAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// Remove uma conversa anônima.
        /// </summary>
        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Chat removido com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _chatService.DeleteAsync(id);
            return NoContent();
        }
    }
}