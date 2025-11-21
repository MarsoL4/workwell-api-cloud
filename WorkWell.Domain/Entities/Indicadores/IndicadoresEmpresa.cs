using System.Collections.Generic;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Entities.Indicadores
{
    public class IndicadoresEmpresa
    {
        public long Id { get; set; }
        public long EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }
        public double HumorMedio { get; set; }
        public double AdesaoAtividadesGeral { get; set; }
        public ICollection<AdesaoSetor> AdesaoPorSetor { get; set; } = [];
        public double FrequenciaConsultas { get; set; }
    }

    public class AdesaoSetor
    {
        public long Id { get; set; }
        public long SetorId { get; set; }
        public Setor? Setor { get; set; }
        public double Adesao { get; set; }
    }
}