using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.Indicadores;
using System.Collections.Generic;

namespace WorkWell.API.SwaggerExamples
{
    public class IndicadoresEmpresaDtoExample : IExamplesProvider<IndicadoresEmpresaDto>
    {
        public IndicadoresEmpresaDto GetExamples()
        {
            return new IndicadoresEmpresaDto
            {
                EmpresaId = 1,
                HumorMedio = 4.1,
                AdesaoAtividadesGeral = 0.85,
                FrequenciaConsultas = 0.21,
                AdesaoPorSetor = new List<AdesaoSetorDto>
                {
                    new AdesaoSetorDto
                    {
                        SetorId = 2,
                        Adesao = 0.90
                    }
                }
            };
        }
    }
}