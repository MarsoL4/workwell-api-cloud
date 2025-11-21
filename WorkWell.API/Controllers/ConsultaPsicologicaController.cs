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
    /// Gerencia consultas psicológicas agendadas entre funcionários e psicólogos.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Psicologo", "Admin", "RH", "Funcionario")]
    public class ConsultaPsicologicaController : ControllerBase
    {
        private readonly IConsultaPsicologicaService _consultaService;

        public ConsultaPsicologicaController(IConsultaPsicologicaService consultaService)
        {
            _consultaService = consultaService;
        }

        /// <summary>
        /// Lista todas as consultas.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "Lista de consultas", typeof(IEnumerable<ConsultaPsicologicaDto>))]
        [ProducesResponseType(typeof(IEnumerable<ConsultaPsicologicaDto>), 200)]
        public async Task<ActionResult<IEnumerable<ConsultaPsicologicaDto>>> GetAll()
        {
            var consultas = await _consultaService.GetAllAsync();
            return Ok(consultas);
        }

        /// <summary>
        /// Busca uma consulta por id.
        /// </summary>
        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Consulta encontrada", typeof(ConsultaPsicologicaDto))]
        [SwaggerResponse(404, "Consulta não encontrada")]
        [ProducesResponseType(typeof(ConsultaPsicologicaDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ConsultaPsicologicaDto>> GetById(long id)
        {
            var consulta = await _consultaService.GetByIdAsync(id);
            if (consulta == null)
                return NotFound(new { mensagem = "Consulta não encontrada." });
            return Ok(consulta);
        }

        /// <summary>
        /// Agenda uma nova consulta psicológica.
        /// </summary>
        [HttpPost]
        [SwaggerRequestExample(typeof(ConsultaPsicologicaDto), typeof(ConsultaPsicologicaDtoExample))]
        [SwaggerResponse(201, "Consulta criada com sucesso", typeof(ConsultaPsicologicaDto))]
        [SwaggerResponse(400, "Dados inválidos")]
        [ProducesResponseType(typeof(ConsultaPsicologicaDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ConsultaPsicologicaDto>> Create(ConsultaPsicologicaDto dto)
        {
            var id = await _consultaService.CreateAsync(dto);
            var consultaCriada = await _consultaService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, consultaCriada);
        }

        /// <summary>
        /// Atualiza os dados de uma consulta.
        /// </summary>
        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(ConsultaPsicologicaDto), typeof(ConsultaPsicologicaDtoExample))]
        [SwaggerResponse(204, "Consulta atualizada com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [SwaggerResponse(404, "Consulta não encontrada")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(long id, ConsultaPsicologicaDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { mensagem = "ID da URL e do objeto devem coincidir." });

            var consulta = await _consultaService.GetByIdAsync(id);
            if (consulta == null)
                return NotFound(new { mensagem = "Consulta não encontrada." });

            await _consultaService.UpdateAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// Remove uma consulta.
        /// </summary>
        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Consulta removida com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _consultaService.DeleteAsync(id);
            return NoContent();
        }
    }
}