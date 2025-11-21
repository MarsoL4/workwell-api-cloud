using System;

namespace WorkWell.Application.DTOs.OmbudMind
{
    public class InvestigacaoDenunciaDto
    {
        public long Id { get; set; }
        public long DenunciaId { get; set; }
        public string EquipeResponsavel { get; set; } = null!;
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string MedidasAdotadas { get; set; } = null!;
        public bool Concluida { get; set; }
    }
}