using System.Collections.Generic;

namespace WorkWell.Application.DTOs.Indicadores
{
    public class IndicadoresEmpresaDto
    {
        public long Id { get; set; }
        public long EmpresaId { get; set; }
        public double HumorMedio { get; set; }
        public double AdesaoAtividadesGeral { get; set; }
        public List<AdesaoSetorDto> AdesaoPorSetor { get; set; } = [];
        public double FrequenciaConsultas { get; set; }
    }

    public class AdesaoSetorDto
    {
        public long Id { get; set; }
        public long SetorId { get; set; }
        public double Adesao { get; set; }
    }
}