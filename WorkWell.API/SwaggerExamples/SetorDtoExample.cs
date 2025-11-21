using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.EmpresaOrganizacao;

namespace WorkWell.API.SwaggerExamples
{
    public class SetorDtoExample : IExamplesProvider<SetorDto>
    {
        public SetorDto GetExamples()
        {
            // Para POST, não enviar Id
            return new SetorDto
            {
                Nome = "RH",
                EmpresaId = 1
            };
        }
    }
}