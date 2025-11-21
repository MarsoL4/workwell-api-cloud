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
    /// Gerencia funcionários cadastrados na empresa (Admin/RH).
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiKeyAuthorize("Admin", "RH")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionarioController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        /// <summary>
        /// Lista paginada de funcionários.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "Lista paginada de funcionários", typeof(PagedResultDto<FuncionarioDto>))]
        [ProducesResponseType(typeof(PagedResultDto<FuncionarioDto>), 200)]
        public async Task<ActionResult<PagedResultDto<FuncionarioDto>>> GetAllPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _funcionarioService.GetAllPagedAsync(page, pageSize);
            result.Links = HateoasLinkBuilder.BuildPagedLinks(HttpContext, result.Page, result.PageSize, result.TotalCount);
            return Ok(result);
        }

        /// <summary>
        /// Busca funcionário por Id.
        /// </summary>
        [HttpGet("{id:long}")]
        [SwaggerResponse(200, "Funcionário encontrado", typeof(FuncionarioDto))]
        [SwaggerResponse(404, "Funcionário não encontrado")]
        [ProducesResponseType(typeof(FuncionarioDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<FuncionarioDto>> GetById(long id)
        {
            var funcionario = await _funcionarioService.GetByIdAsync(id);
            if (funcionario == null)
                return NotFound(new { mensagem = "Funcionário não encontrado." });
            return Ok(funcionario);
        }

        /// <summary>
        /// Cadastra funcionário.
        /// </summary>
        [HttpPost]
        [SwaggerRequestExample(typeof(FuncionarioDto), typeof(FuncionarioDtoExample))]
        [SwaggerResponse(201, "Funcionário criado com sucesso", typeof(FuncionarioDto))]
        [ProducesResponseType(typeof(FuncionarioDto), 201)]
        public async Task<ActionResult<FuncionarioDto>> Create(FuncionarioDto funcionarioDto)
        {
            var id = await _funcionarioService.CreateAsync(funcionarioDto);
            var funcionarioCriado = await _funcionarioService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, funcionarioCriado);
        }

        /// <summary>
        /// Atualiza funcionário.
        /// </summary>
        [HttpPut("{id:long}")]
        [SwaggerRequestExample(typeof(FuncionarioDto), typeof(FuncionarioDtoExample))]
        [SwaggerResponse(204, "Funcionário atualizado com sucesso")]
        [SwaggerResponse(400, "IDs não coincidem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(long id, FuncionarioDto funcionarioDto)
        {
            if (id != funcionarioDto.Id)
                return BadRequest(new { mensagem = "ID da URL e do objeto devem coincidir." });

            await _funcionarioService.UpdateAsync(funcionarioDto);
            return NoContent();
        }

        /// <summary>
        /// Remove funcionário.
        /// </summary>
        [HttpDelete("{id:long}")]
        [SwaggerResponse(204, "Funcionário removido com sucesso")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _funcionarioService.DeleteAsync(id);
            return NoContent();
        }
    }
}