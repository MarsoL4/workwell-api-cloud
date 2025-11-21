using System;
using System.ComponentModel.DataAnnotations;

namespace WorkWell.Domain.Entities.OmbudMind
{
    public class InvestigacaoDenuncia
    {
        public long Id { get; set; }
        public long DenunciaId { get; set; }
        public Denuncia? Denuncia { get; set; }

        [MaxLength(100)]
        public string EquipeResponsavel { get; set; } = null!;

        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        [MaxLength(1000)]
        public string MedidasAdotadas { get; set; } = null!;

        public bool Concluida { get; set; }
    }
}