namespace WorkWell.Application.DTOs.AvaliacoesEmocionais
{
    public class RiscoPsicossocialDto
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public string Categoria { get; set; } = null!;
        public int NivelRisco { get; set; }
        public DateTime? DataRegistro { get; set; }
    }
}