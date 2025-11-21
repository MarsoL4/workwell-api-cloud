using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WorkWell.API.Helpers;
using WorkWell.API.Security;
using WorkWell.API.SwaggerExamples;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Application.DTOs.Paginacao;
using WorkWell.Application.Services.EmpresaOrganizacao;

namespace WorkWell.API.Controllers
{
    /// <summary>
    /// Gerenciamento de setores das empresas para segmentação dos relatórios e atividades.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Admin", "RH")]
    public class SetorController : ControllerBase
    {
        private readonly ISetorService _setorService;

        public SetorController(ISetorService setorService)
        {
            _setorService = setorService;
        }

        [HttpGet]
        [SwaggerResponse(200, "Lista paginada de setores", typeof(PagedResultDto<SetorDto>))]
        [ProducesResponseType(typeof(PagedResultDto<SetorDto>), 200)]
        public async Task<ActionResult<PagedResultDto<SetorDto>>> GetAllPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _setorService.GetAllPagedAsync(page, pageSize);
            result.Links = HateoasLinkBuilder.BuildPagedLinks(HttpContext, result.Page, result.PageSize, result.TotalCount);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Setor encontrado", typeof(SetorDto))]
        [SwaggerResponse(404, "Setor não encontrado")]
        [ProducesResponseType(typeof(SetorDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SetorDto>> GetById(long id)
        {
            var setor = await _setorService.GetByIdAsync(id);
            if (setor == null)
                return NotFound(new { mensagem = "Setor não encontrado." });
            return Ok(setor);
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(SetorDto), typeof(SetorDtoExample))]
        [SwaggerResponse(201, "Setor criado com sucesso", typeof(SetorDto))]
        [ProducesResponseType(typeof(SetorDto), 201)]
        public async Task<ActionResult<SetorDto>> Create(SetorDto setorDto)
        {
            var id = await _setorService.CreateAsync(setorDto);
            var setorCriado = await _setorService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, setorCriado);
        }

        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(SetorDto), typeof(SetorDtoExample))]
        [SwaggerResponse(204, "Setor atualizado com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(long id, SetorDto setorDto)
        {
            if (id != setorDto.Id)
                return BadRequest(new { mensagem = "ID da URL e do objeto devem coincidir." });

            await _setorService.UpdateAsync(setorDto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Setor removido com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _setorService.DeleteAsync(id);
            return NoContent();
        }
    }
}