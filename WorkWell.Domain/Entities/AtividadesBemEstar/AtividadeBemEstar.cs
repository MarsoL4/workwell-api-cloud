using System;
using System.ComponentModel.DataAnnotations;
using WorkWell.Domain.Entities.EmpresaOrganizacao;
using WorkWell.Domain.Enums.AtividadesBemEstar;

namespace WorkWell.Domain.Entities.AtividadesBemEstar
{
    public class AtividadeBemEstar
    {
        public long Id { get; set; }
        public long EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }
        public TipoAtividade Tipo { get; set; }

        [MaxLength(100)]
        public string Titulo { get; set; } = null!;

        [MaxLength(1000)]
        public string Descricao { get; set; } = null!;

        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public long? SetorAlvoId { get; set; }
        public Setor? SetorAlvo { get; set; }
    }
}