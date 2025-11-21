namespace WorkWell.Application.DTOs.AvaliacoesEmocionais
{
    public class AvaliacaoProfundaDto
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public int Gad7Score { get; set; }
        public int? Phq9Score { get; set; }
        public string Interpretacao { get; set; } = null!;
        public DateTime? DataRegistro { get; set; }
    }
}