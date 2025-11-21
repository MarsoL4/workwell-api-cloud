namespace WorkWell.Application.DTOs.AvaliacoesEmocionais
{
    public class MoodCheckDto
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public int Humor { get; set; }
        public bool Produtivo { get; set; }
        public bool Estressado { get; set; }
        public bool DormiuBem { get; set; }
        public DateTime? DataRegistro { get; set; }
    }
}