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
    /// Perfil emocional criado no onboarding ou revisado pelo psicólogo.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Funcionario", "Psicologo", "RH")]
    public class PerfilEmocionalController : ControllerBase
    {
        private readonly IPerfilEmocionalService _perfilService;

        public PerfilEmocionalController(IPerfilEmocionalService perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpGet]
        [SwaggerResponse(200, "Lista de perfis emocionais", typeof(IEnumerable<PerfilEmocionalDto>))]
        [ProducesResponseType(typeof(IEnumerable<PerfilEmocionalDto>), 200)]
        public async Task<ActionResult<IEnumerable<PerfilEmocionalDto>>> GetAll()
        {
            var perfis = await _perfilService.GetAllAsync();
            return Ok(perfis);
        }

        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Perfil emocional encontrado", typeof(PerfilEmocionalDto))]
        [SwaggerResponse(404, "Perfil não encontrado")]
        [ProducesResponseType(typeof(PerfilEmocionalDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PerfilEmocionalDto>> GetById(long id)
        {
            var perfil = await _perfilService.GetByIdAsync(id);
            if (perfil == null)
                return NotFound(new { mensagem = "Perfil não encontrado." });
            return Ok(perfil);
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(PerfilEmocionalDto), typeof(PerfilEmocionalDtoExample))]
        [SwaggerResponse(201, "Perfil criado com sucesso", typeof(PerfilEmocionalDto))]
        [ProducesResponseType(typeof(PerfilEmocionalDto), 201)]
        public async Task<ActionResult<PerfilEmocionalDto>> Create(PerfilEmocionalDto dto)
        {
            var id = await _perfilService.CreateAsync(dto);
            var perfilCriado = await _perfilService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, perfilCriado);
        }

        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(PerfilEmocionalDto), typeof(PerfilEmocionalDtoExample))]
        [SwaggerResponse(204, "Perfil atualizado com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(long id, PerfilEmocionalDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { mensagem = "ID da URL e do objeto devem coincidir." });

            await _perfilService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Perfil removido com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _perfilService.DeleteAsync(id);
            return NoContent();
        }
    }
}