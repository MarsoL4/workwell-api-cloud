using System;
using WorkWell.Domain.Enums.AtividadesBemEstar;

namespace WorkWell.Application.DTOs.AtividadesBemEstar
{
    public class AtividadeBemEstarDto
    {
        public long Id { get; set; }
        public long EmpresaId { get; set; }
        public TipoAtividade Tipo { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public long? SetorAlvoId { get; set; }
    }
}