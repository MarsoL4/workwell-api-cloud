using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.ApoioPsicologico;

namespace WorkWell.API.SwaggerExamples
{
    public class PsicologoDtoExample : IExamplesProvider<PsicologoDto>
    {
        public PsicologoDto GetExamples()
        {
            // Exemplo para banco vazio, preenchendo EmpresaId e SetorId válidos/criados!
            return new PsicologoDto
            {
                Nome = "Dra. Helena Alves",
                Email = "helena.alves@futurework.com",
                Senha = "psic123",
                TokenEmpresa = "token-ftw-001",
                Crp = "06/123456",
                Ativo = true,
                SetorId = 1,          // Setor RH
                EmpresaId = 1
            };
        }
    }
}