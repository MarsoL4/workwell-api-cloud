using WorkWell.Domain.Enums.OmbudMind;

namespace WorkWell.Application.DTOs.OmbudMind
{
    public class DenunciaDto
    {
        public long Id { get; set; }
        public long? FuncionarioDenuncianteId { get; set; }
        public long EmpresaId { get; set; }
        public TipoDenuncia Tipo { get; set; }
        public string Descricao { get; set; } = null!;
        public DateTime? DataCriacao { get; set; }
        public StatusDenuncia Status { get; set; }
        public string CodigoRastreamento { get; set; } = null!;
    }
}