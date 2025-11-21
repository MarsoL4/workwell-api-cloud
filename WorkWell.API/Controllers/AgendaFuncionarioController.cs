using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.Agenda;
using WorkWell.Application.Services.Agenda;
using WorkWell.API.Security;
using WorkWell.API.SwaggerExamples;

namespace WorkWell.API.Controllers
{
    /// <summary>
    /// Gerencia agendas de funcionários: permite cadastro diário de atividades, consultas e eventos na agenda pessoal.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Funcionario", "RH", "Admin")]
    public class AgendaFuncionarioController : ControllerBase
    {
        private readonly IAgendaFuncionarioService _agendaService;

        public AgendaFuncionarioController(IAgendaFuncionarioService agendaService)
        {
            _agendaService = agendaService;
        }

        /// <summary>
        /// Lista todas as agendas dos funcionários.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "Lista de agendas", typeof(IEnumerable<AgendaFuncionarioDto>))]
        [ProducesResponseType(typeof(IEnumerable<AgendaFuncionarioDto>), 200)]
        public async Task<ActionResult<IEnumerable<AgendaFuncionarioDto>>> GetAll()
        {
            var agendas = await _agendaService.GetAllAsync();
            return Ok(agendas);
        }

        /// <summary>
        /// Busca uma agenda específica por id.
        /// </summary>
        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Agenda encontrada", typeof(AgendaFuncionarioDto))]
        [SwaggerResponse(404, "Agenda não encontrada")]
        [ProducesResponseType(typeof(AgendaFuncionarioDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AgendaFuncionarioDto>> GetById(long id)
        {
            var agenda = await _agendaService.GetByIdAsync(id);
            if (agenda == null)
                return NotFound(new { mensagem = "Agenda não encontrada." });
            return Ok(agenda);
        }

        /// <summary>
        /// Cadastra uma nova agenda para um funcionário.
        /// </summary>
        /// <remarks>
        /// O campo <b>Id</b> é gerado automaticamente. Inclua o <b>FuncionarioId</b> e os itens do dia desejados.
        /// </remarks>
        [HttpPost]
        [SwaggerRequestExample(typeof(AgendaFuncionarioDto), typeof(AgendaFuncionarioDtoExample))]
        [SwaggerResponse(201, "Agenda criada com sucesso", typeof(AgendaFuncionarioDto))]
        [SwaggerResponse(400, "Dados inválidos")]
        [ProducesResponseType(typeof(AgendaFuncionarioDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<AgendaFuncionarioDto>> Create(AgendaFuncionarioDto dto)
        {
            var id = await _agendaService.CreateAsync(dto);
            var agendaCriada = await _agendaService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, agendaCriada);
        }

        /// <summary>
        /// Atualiza uma agenda existente.
        /// </summary>
        /// <param name="id">Id da agenda</param>
        /// <param name="dto">Novos dados da agenda</param>
        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(AgendaFuncionarioDto), typeof(AgendaFuncionarioDtoExample))]
        [SwaggerResponse(204, "Agenda atualizada com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(long id, AgendaFuncionarioDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { mensagem = "ID da URL e do objeto devem coincidir." });

            await _agendaService.UpdateAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// Remove uma agenda.
        /// </summary>
        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Agenda removida com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _agendaService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Lista os itens de uma agenda específica.
        /// </summary>
        [HttpGet("{agendaId:long}/itens")]
        [SwaggerResponse(200, "Lista de itens da agenda", typeof(IEnumerable<ItemAgendaDto>))]
        [ProducesResponseType(typeof(IEnumerable<ItemAgendaDto>), 200)]
        public async Task<ActionResult<IEnumerable<ItemAgendaDto>>> GetItens(long agendaId)
        {
            var itens = await _agendaService.GetItensAsync(agendaId);
            return Ok(itens);
        }

        /// <summary>
        /// Adiciona um novo item à agenda.
        /// </summary>
        /// <remarks>
        /// O campo <b>Id</b> é gerado automaticamente; é necessário informar título, tipo e horário.
        /// </remarks>
        [HttpPost("{agendaId:long}/itens")]
        [SwaggerRequestExample(typeof(ItemAgendaDto), typeof(ItemAgendaDtoExample))]
        [SwaggerResponse(200, "Item criado com sucesso", typeof(ItemAgendaDto))]
        [ProducesResponseType(typeof(ItemAgendaDto), 200)]
        public async Task<ActionResult<ItemAgendaDto>> AdicionarItem(long agendaId, ItemAgendaDto dto)
        {
            var id = await _agendaService.AdicionarItemAsync(agendaId, dto);
            var itemCriado = (await _agendaService.GetItensAsync(agendaId)).FirstOrDefault(i => i.Id == id);
            return Ok(itemCriado);
        }

        /// <summary>
        /// Atualiza um item da agenda.
        /// </summary>
        [HttpPut("{agendaId:long}/itens/{itemId:long}")]
        [SwaggerRequestExample(typeof(ItemAgendaDto), typeof(ItemAgendaDtoExample))]
        [SwaggerResponse(204, "Item da agenda atualizado com sucesso")]
        [SwaggerResponse(400, "IDs inconsistentes")]
        [SwaggerResponse(404, "Item não encontrado")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateItem(long agendaId, long itemId, ItemAgendaDto dto)
        {
            if (itemId != dto.Id)
                return BadRequest(new { mensagem = "ID do item da URL e do objeto devem coincidir." });

            // Service verifica se pertence à agenda
            var atualizou = await _agendaService.UpdateItemAsync(agendaId, dto);
            if (!atualizou)
                return NotFound(new { mensagem = "Item da agenda não encontrado para a agenda informada." });
            return NoContent();
        }

        /// <summary>
        /// Remove um item da agenda.
        /// </summary>
        [HttpDelete("{agendaId:long}/itens/{itemId:long}")]
        [SwaggerResponse(204, "Item da agenda removido com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteItem(long agendaId, long itemId)
        {
            var removeu = await _agendaService.DeleteItemAsync(agendaId, itemId);
            if (!removeu)
                return NotFound(new { mensagem = "Item da agenda não encontrado para a agenda informada." });
            return NoContent();
        }
    }
}