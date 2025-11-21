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
    /// Permite autoavaliação diária do humor para acompanhamento emocional do funcionário.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Funcionario", "Psicologo", "RH")]
    public class MoodCheckController : ControllerBase
    {
        private readonly IMoodCheckService _moodService;

        public MoodCheckController(IMoodCheckService moodService)
        {
            _moodService = moodService;
        }

        [HttpGet]
        [SwaggerResponse(200, "Lista de registros de humor", typeof(IEnumerable<MoodCheckDto>))]
        [ProducesResponseType(typeof(IEnumerable<MoodCheckDto>), 200)]
        public async Task<ActionResult<IEnumerable<MoodCheckDto>>> GetAll()
        {
            var registros = await _moodService.GetAllAsync();
            return Ok(registros);
        }

        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Registro de humor encontrado", typeof(MoodCheckDto))]
        [SwaggerResponse(404, "Registro não encontrado")]
        [ProducesResponseType(typeof(MoodCheckDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<MoodCheckDto>> GetById(long id)
        {
            var entity = await _moodService.GetByIdAsync(id);
            if (entity == null)
                return NotFound(new { mensagem = "Registro não encontrado." });
            return Ok(entity);
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(MoodCheckDto), typeof(MoodCheckDtoExample))]
        [SwaggerResponse(201, "Registro criado com sucesso", typeof(MoodCheckDto))]
        [ProducesResponseType(typeof(MoodCheckDto), 201)]
        public async Task<ActionResult<MoodCheckDto>> Create(MoodCheckDto dto)
        {
            var id = await _moodService.CreateAsync(dto);
            var registroCriado = await _moodService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, registroCriado);
        }

        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(MoodCheckDto), typeof(MoodCheckDtoExample))]
        [SwaggerResponse(204, "Registro atualizado com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(long id, MoodCheckDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { mensagem = "ID da URL e do objeto devem coincidir." });

            await _moodService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Registro removido com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _moodService.DeleteAsync(id);
            return NoContent();
        }
    }
}