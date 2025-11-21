using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Application.Services.EmpresaOrganizacao;
using WorkWell.Application.DTOs.Paginacao;
using WorkWell.API.Security;
using WorkWell.API.SwaggerExamples;
using WorkWell.API.Helpers;

namespace WorkWell.API.Controllers
{
    /// <summary>
    /// Gerencia as empresas cadastradas no sistema (Admin).
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Admin")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;
        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        /// <summary>
        /// Retorna uma lista paginada de empresas cadastradas.
        /// </summary>
        /// <remarks>
        /// Permite paginação com os parâmetros <b>page</b> e <b>pageSize</b>.
        /// Apenas Admin pode visualizar todas as empresas.
        /// </remarks>
        /// <returns>Lista paginada de empresas</returns>
        [HttpGet]
        [SwaggerResponse(200, "Lista paginada de empresas", typeof(PagedResultDto<EmpresaDto>))]
        [ProducesResponseType(typeof(PagedResultDto<EmpresaDto>), 200)]
        public async Task<ActionResult<PagedResultDto<EmpresaDto>>> GetAllPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _empresaService.GetAllPagedAsync(page, pageSize);

            // Gera os links HATEOAS
            result.Links = HateoasLinkBuilder.BuildPagedLinks(HttpContext, result.Page, result.PageSize, result.TotalCount);

            return Ok(result);
        }

        /// <summary>
        /// Busca uma empresa por ID.
        /// </summary>
        /// <param name="id">ID da empresa</param>
        /// <returns>Dados da empresa</returns>
        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Empresa encontrada", typeof(EmpresaDto))]
        [SwaggerResponse(404, "Empresa não encontrada")]
        [ProducesResponseType(typeof(EmpresaDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<EmpresaDto>> GetById(long id)
        {
            var empresa = await _empresaService.GetByIdAsync(id);
            if (empresa == null)
                return NotFound(new { mensagem = "Empresa não encontrada." });
            return Ok(empresa);
        }

        /// <summary>
        /// Cadastra uma nova empresa.
        /// </summary>
        /// <remarks>
        /// Só o Admin pode cadastrar. O campo ID é gerado automaticamente.
        /// </remarks>
        /// <returns>ID da empresa criada</returns>
        [HttpPost]
        [SwaggerRequestExample(typeof(EmpresaDto), typeof(EmpresaDtoExample))]
        [SwaggerResponse(201, "Empresa criada com sucesso", typeof(EmpresaDto))]
        [SwaggerResponse(400, "Dados inválidos")]
        [ProducesResponseType(typeof(EmpresaDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<EmpresaDto>> Create(EmpresaDto empresaDto)
        {
            var id = await _empresaService.CreateAsync(empresaDto);
            var empresaCriada = await _empresaService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, empresaCriada);
        }

        /// <summary>
        /// Atualiza uma empresa existente pelo ID.
        /// </summary>
        /// <returns>NoContent</returns>
        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(EmpresaDto), typeof(EmpresaDtoExample))]
        [SwaggerResponse(204, "Empresa atualizada com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [SwaggerResponse(404, "Empresa não encontrada")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(long id, EmpresaDto empresaDto)
        {
            if (id != empresaDto.Id)
                return BadRequest(new { mensagem = "ID da URL e do corpo devem coincidir." });

            var empresa = await _empresaService.GetByIdAsync(id);
            if (empresa == null)
                return NotFound(new { mensagem = "Empresa não encontrada." });

            await _empresaService.UpdateAsync(empresaDto);
            return NoContent();
        }

        /// <summary>
        /// Remove uma empresa por ID.
        /// </summary>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Empresa removida com sucesso")]
        [SwaggerResponse(404, "Empresa não encontrada")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(long id)
        {
            var empresa = await _empresaService.GetByIdAsync(id);
            if (empresa == null)
                return NotFound(new { mensagem = "Empresa não encontrada." });

            await _empresaService.DeleteAsync(id);
            return NoContent();
        }
    }
}