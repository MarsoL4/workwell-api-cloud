using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Domain.Enums.EmpresaOrganizacao;

namespace WorkWell.API.SwaggerExamples
{
    public class FuncionarioDtoExample : IExamplesProvider<FuncionarioDto>
    {
        public FuncionarioDto GetExamples()
        {
            // Para criar um novo funcionário (POST): não enviar Id
            return new FuncionarioDto
            {
                Nome = "Carlos Silva",
                Email = "carlos@futurework.com",
                Senha = "func123",
                TokenEmpresa = "token-ftw-001",
                Cargo = Cargo.Funcionario,
                Ativo = true,
                SetorId = 2 // TI
            };
        }
    }
}