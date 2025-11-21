using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.EmpresaOrganizacao;

namespace WorkWell.API.SwaggerExamples
{
    public class EmpresaDtoExample : IExamplesProvider<EmpresaDto>
    {
        public EmpresaDto GetExamples()
        {
            return new EmpresaDto
            {
                Nome = "Futuro do Trabalho Ltda",
                EmailAdmin = "admin@futurework.com",
                SenhaAdmin = "admin123",
                TokenAcesso = "token-ftw-001",
                LogoUrl = "https://futurework.com/logo.png",
                CorPrimaria = "#1F77B4",
                CorSecundaria = "#FFB800",
                Missao = "Transformar o bem-estar no ambiente de trabalho.",
                PoliticaBemEstar = "Aqui o respeito e o cuidado são prioridades!"
            };
        }
    }
}