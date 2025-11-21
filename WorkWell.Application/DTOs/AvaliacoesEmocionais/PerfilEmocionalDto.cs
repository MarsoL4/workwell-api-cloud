namespace WorkWell.Application.DTOs.AvaliacoesEmocionais
{
    public class PerfilEmocionalDto
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public string HumorInicial { get; set; } = null!;
        public string Rotina { get; set; } = null!;
        public string PrincipaisEstressores { get; set; } = null!;
        public DateTime? DataCriacao { get; set; }
    }
}