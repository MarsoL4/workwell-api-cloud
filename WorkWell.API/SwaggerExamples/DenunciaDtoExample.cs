using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.OmbudMind;
using WorkWell.Domain.Enums.OmbudMind;

namespace WorkWell.API.SwaggerExamples
{
    public class DenunciaDtoExample : IExamplesProvider<DenunciaDto>
    {
        public DenunciaDto GetExamples()
        {
            // Em POST não enviar Id/CodigoRastreamento/DataCriacao
            return new DenunciaDto
            {
                FuncionarioDenuncianteId = 4, // Carlos Silva
                EmpresaId = 1,
                Tipo = TipoDenuncia.AssedioMoral,
                Descricao = "Relato de assédio moral pelo gestor.",
                Status = StatusDenuncia.Aberta,
                CodigoRastreamento = "WW-2024-0001"
            };
        }
    }
}